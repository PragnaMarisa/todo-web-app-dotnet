# Makefile for todo-web-app-dotnet
# ASP.NET Core MVC Task Management Application
#
# Usage: make [target]
# Example: make run

.PHONY: help install restore build run test clean migration-add migration-update migration-remove migration-list docker-build docker-run watch dev

# Default target - show help
help:
	@echo "=================================================="
	@echo "  Task Master - Available Commands"
	@echo "=================================================="
	@echo ""
	@echo "Getting Started:"
	@echo "  make install          - Install project dependencies"
	@echo "  make restore          - Restore NuGet packages"
	@echo "  make setup            - First-time setup (restore + migrate)"
	@echo ""
	@echo "Development:"
	@echo "  make build            - Build the project"
	@echo "  make run              - Run the application"
	@echo "  make watch            - Run with hot reload (auto-restart on changes)"
	@echo "  make dev              - Alias for watch"
	@echo ""
	@echo "Database Migrations:"
	@echo "  make migration-add NAME=MigrationName    - Create new migration"
	@echo "  make migration-update                    - Apply pending migrations"
	@echo "  make migration-remove                    - Remove last migration"
	@echo "  make migration-list                      - List all migrations"
	@echo "  make db-reset                            - Drop database and reapply migrations"
	@echo ""
	@echo "Testing:"
	@echo "  make test             - Run all tests"
	@echo "  make test-verbose     - Run tests with detailed output"
	@echo "  make coverage         - Run tests with code coverage report"
	@echo ""
	@echo "Docker:"
	@echo "  make docker-build     - Build Docker image"
	@echo "  make docker-run       - Run application in Docker container"
	@echo ""
	@echo "Cleanup:"
	@echo "  make clean            - Clean build artifacts"
	@echo "  make clean-all        - Clean everything (bin, obj, packages)"
	@echo ""
	@echo "=================================================="

# ============================================
# Project Setup
# ============================================

# Install .NET SDK dependencies (restore packages)
install:
	@echo "📦 Restoring NuGet packages..."
	dotnet restore

# Restore NuGet packages (alias for install)
restore:
	@echo "📦 Restoring NuGet packages..."
	dotnet restore

# First-time setup: restore packages and apply migrations
setup: restore
	@echo "🚀 Setting up project for first time..."
	@echo "⚠️  Make sure your PostgreSQL connection string is configured in appsettings.Development.json"
	@echo ""
	@echo "Applying database migrations..."
	dotnet ef database update
	@echo ""
	@echo "✅ Setup complete! Run 'make run' to start the application."

# ============================================
# Build & Run
# ============================================

# Build the project in Release mode
build:
	@echo "🔨 Building project..."
	dotnet build --configuration Release

# Run the application
run:
	@echo "🚀 Starting application..."
	@echo "📍 Application will be available at: https://localhost:5001"
	dotnet run

# Run with hot reload (automatically restarts on file changes)
watch:
	@echo "🔥 Starting application with hot reload..."
	@echo "📍 Application will be available at: https://localhost:5001"
	@echo "💡 Files will be watched for changes and app will auto-restart"
	dotnet watch run

# Alias for watch (common convention)
dev: watch

# ============================================
# Database Migrations
# ============================================

# Create a new migration
# Usage: make migration-add NAME=InitialCreate
migration-add:
ifndef NAME
	@echo "❌ Error: Migration name required"
	@echo "Usage: make migration-add NAME=YourMigrationName"
	@echo "Example: make migration-add NAME=AddUserTable"
	@exit 1
endif
	@echo "📝 Creating migration: $(NAME)..."
	dotnet ef migrations add $(NAME)
	@echo "✅ Migration '$(NAME)' created successfully"
	@echo "💡 Run 'make migration-update' to apply it to the database"

# Apply pending migrations to the database
migration-update:
	@echo "⬆️  Applying database migrations..."
	dotnet ef database update
	@echo "✅ Database updated successfully"

# Remove the last migration (only if not applied to database)
migration-remove:
	@echo "⚠️  Removing last migration..."
	dotnet ef migrations remove
	@echo "✅ Last migration removed"

# List all migrations
migration-list:
	@echo "📋 All migrations:"
	dotnet ef migrations list

# Drop database and reapply all migrations (CAUTION: Data loss!)
db-reset:
	@echo "⚠️  WARNING: This will drop the database and recreate it!"
	@echo "⚠️  All data will be lost!"
	@read -p "Are you sure? (yes/no): " confirm; \
	if [ "$$confirm" = "yes" ]; then \
		echo "🗑️  Dropping database..."; \
		dotnet ef database drop --force; \
		echo "⬆️  Reapplying migrations..."; \
		dotnet ef database update; \
		echo "✅ Database reset complete"; \
	else \
		echo "❌ Cancelled"; \
	fi

# ============================================
# Testing
# ============================================

# Run all tests
test:
	@echo "🧪 Running tests..."
	dotnet test --configuration Release

# Run tests with verbose output
test-verbose:
	@echo "🧪 Running tests with detailed output..."
	dotnet test --configuration Release --verbosity detailed

# Run tests with code coverage (requires coverlet)
coverage:
	@echo "📊 Running tests with code coverage..."
	@if ! dotnet tool list -g | grep -q coverlet.console; then \
		echo "📦 Installing coverlet.console..."; \
		dotnet tool install -g coverlet.console; \
	fi
	dotnet test --configuration Release \
		/p:CollectCoverage=true \
		/p:CoverletOutputFormat=opencover \
		/p:CoverletOutput=./TestResults/coverage.xml
	@echo "✅ Coverage report generated in ./TestResults/"
	@echo "💡 You can view the coverage.xml file with a coverage viewer"

# ============================================
# Docker
# ============================================

# Build Docker image
docker-build:
	@echo "🐳 Building Docker image..."
	docker build -t todo-web-app-dotnet:latest .
	@echo "✅ Docker image built successfully"
	@echo "💡 Run 'make docker-run' to start the container"

# Run application in Docker container
# Note: Replace with your actual PostgreSQL connection string
docker-run:
	@echo "🐳 Running Docker container..."
	@echo "⚠️  Make sure to set your PostgreSQL connection string"
	docker run -p 8080:8080 \
		-e ASPNETCORE_ENVIRONMENT=Production \
		-e ConnectionStrings__DefaultConnection="Host=your-host;Database=your-db;Username=your-user;Password=your-pass" \
		todo-web-app-dotnet:latest
	@echo "📍 Application available at: http://localhost:8080"

# ============================================
# Cleanup
# ============================================

# Clean build artifacts
clean:
	@echo "🧹 Cleaning build artifacts..."
	dotnet clean
	@echo "✅ Clean complete"

# Clean everything (bin, obj, packages, test results)
clean-all: clean
	@echo "🧹 Deep cleaning project..."
	@echo "Removing bin/ directories..."
	find . -type d -name "bin" -exec rm -rf {} + 2>/dev/null || true
	@echo "Removing obj/ directories..."
	find . -type d -name "obj" -exec rm -rf {} + 2>/dev/null || true
	@echo "Removing TestResults/ directories..."
	find . -type d -name "TestResults" -exec rm -rf {} + 2>/dev/null || true
	@echo "✅ Deep clean complete"
	@echo "💡 Run 'make restore' to reinstall packages"

# ============================================
# Quick Start Commands
# ============================================

# For first-time users who just cloned the repo
.PHONY: quickstart
quickstart:
	@echo "🎉 Welcome to Task Master!"
	@echo ""
	@echo "Setting up your development environment..."
	@echo ""
	@$(MAKE) restore
	@echo ""
	@echo "✅ Setup complete!"
	@echo ""
	@echo "Next steps:"
	@echo "  1. Configure your PostgreSQL connection in appsettings.Development.json"
	@echo "  2. Run: make migration-update (to create database tables)"
	@echo "  3. Run: make run (to start the application)"
	@echo ""
	@echo "Or simply run: make dev (for hot reload during development)"
	@echo ""
