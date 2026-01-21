# Deploy ASP.NET MVC App to Render

This guide explains how to deploy the **todo-web-app-dotnet** ASP.NET MVC application to **Render** using Docker.

---

## Prerequisites

* GitHub account
* Render account (sign in with GitHub)
* Project pushed to a GitHub repository
* .NET SDK compatible with your project (locally)

---

## Project Structure (important parts)

```
todo-web-app-dotnet/
├── Controllers/
├── Data/
├── Models/
├── Views/
│   └── Todos/
│       └── AllTodos.cshtml
├── wwwroot/
├── Program.cs
├── todo-web-app-dotnet.csproj
├── Dockerfile
```

---

## 1. Add a Dockerfile

Create a file named **`Dockerfile`** in the project root:

```dockerfile
# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . .
RUN dotnet publish -c Release -o /out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /out .
ENV ASPNETCORE_URLS=http://0.0.0.0:10000
EXPOSE 10000

ENTRYPOINT ["dotnet", "todo-web-app-dotnet.dll"]
```

Commit and push:

```bash
git add Dockerfile
git commit -m "Add Dockerfile for Render"
git push
```

---

## 2. Program.cs Configuration

Ensure `Program.cs` includes static files and correct routing:

```csharp
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Todos}/{action=Index}/{id?}");

app.Run();
```

---

## 3. Controller Setup (Important)

Your `Index` action **must return an existing view**.

Recommended `TodosController` setup:

```csharp
public IActionResult Index()
{
    var todos = _todoService.GetAllTodos();
    return View("AllTodos", todos);
}
```

This ensures `/` loads correctly on Render.

---

## 4. Push Final Changes

```bash
git add .
git commit -m "Prepare app for Render deployment"
git push
```

---

## 5. Create the Render Web Service

1. Go to [https://render.com](https://render.com)
2. Click **New +** → **Web Service**
3. Connect your GitHub repository
4. Configure:

   * **Name**: todo-web-app-dotnet
   * **Branch**: main
   * **Runtime**: Docker
   * **Region**: closest to you

---

## 6. Environment Variables

In Render → **Environment** tab, add:

```
ASPNETCORE_ENVIRONMENT=Production
```

(Render automatically provides the PORT; the Dockerfile already binds correctly.)

---

## 7. Deploy

Click **Create Web Service**.

Render will:

* Build the Docker image
* Run `dotnet publish`
* Start the app

First deploy may take a few minutes.

---

## 8. Access Your App

Once deployed, Render provides a URL like:

```
https://todo-web-app-dotnet.onrender.com
```

Visiting `/` should load the Todos page.

---

## Troubleshooting

### 404 on home page

* Ensure `Index()` returns `AllTodos`
* Ensure `Views/Todos/AllTodos.cshtml` exists

### App crashes on startup

* Check Render **Logs**
* Ensure Dockerfile uses correct DLL name

### Static files not loading

* Confirm `app.UseStaticFiles()` is present
* Verify assets are under `wwwroot/`

---

## Notes

* GitHub Pages **cannot** host ASP.NET apps
* Render free tier may sleep when idle
* Data stored in memory will reset on redeploy

---

## Done 🎉

Your ASP.NET MVC app is now live on Render.
