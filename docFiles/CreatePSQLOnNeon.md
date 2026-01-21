# Creating a PostgreSQL Database on Neon

This document is a **clear, step-by-step reference** for creating a PostgreSQL database using **Neon**. It focuses on *conceptual clarity* rather than commands, so you understand **what you are doing and why**.

---

## 1. What You Are Doing (Big Picture)

You are provisioning a **cloud-hosted PostgreSQL database** that your backend application (for example, ASP.NET Core with EF Core) can connect to.

Key idea:

> You are **not installing PostgreSQL**. You are **requesting** a managed PostgreSQL instance.

Neon handles:

* Server setup
* Backups
* Scaling
* PostgreSQL upgrades

---

## 2. Create / Sign In to Neon Account

* Go to Neon
* Sign up or sign in using GitHub, Google, or email

This account is the **owner** of all databases you create.

Think of this as:

> The cloud identity under which your databases live

---

## 3. Create a Neon Project

After signing in, create a **new project**.

A Neon project represents:

* One application or backend system
* A logical container for databases and users

During project creation:

* Neon automatically provisions PostgreSQL
* You do not choose hardware or operating system

Mental model:

> **Project = a box that holds your database world**

---

## 4. PostgreSQL Database Is Created Automatically

Inside the project, Neon creates:

* A PostgreSQL **database**

This database:

* Is empty by default
* Will later store tables, indexes, and relations

Important:

> Neon creates the database, but **your code defines the schema** (via EF Core migrations).

---

## 5. Database User & Credentials

Neon also creates:

* A **database user**
* A **password** for that user

This user:

* Is used by your application
* Is separate from your Neon login account

Purpose:

> Control who can access the database

---

## 6. Connection Details (Most Important Output)

Neon provides the following connection information:

* Host
* Port
* Database name
* Username
* Password
* SSL requirement

Together, these form the **PostgreSQL connection string**.

Mental model:

> Connection string = instructions that tell your app *where the database is and how to enter it*

You will later place this connection string into your application configuration.

---

## 7. Branch Concept (Neon-Specific)

Neon databases use **branches**, similar to Git.

* `main` branch is created by default
* Each branch has its own isolated data

For beginners:

* Use only the `main` branch
* Ignore branching until later

Mental model:

> **Branch = timeline/version of your database**

---

## 8. What You Do NOT Do in Neon

You do NOT:

* Install PostgreSQL manually
* Create tables by hand
* Manage backups
* Handle database upgrades

Neon manages all infrastructure concerns.

Your responsibility starts at:

* Writing application code
* Running database migrations

---

## 9. End-to-End Mental Flow

```
Neon Account
   ↓
Project
   ↓
PostgreSQL Database
   ↓
Database User
   ↓
Connection String
   ↓
Your Application
```

If you remember this flow, you understand Neon.

---

## 10. How This Fits with ASP.NET Core & EF Core

* Neon hosts the PostgreSQL database
* ASP.NET Core reads the connection string
* EF Core:

  * Creates tables
  * Updates schema
  * Talks to PostgreSQL

Neon = infrastructure
EF Core = schema & data logic

---

## Summary (One-Line Memory Hook)

> **Neon gives you a ready PostgreSQL database; your code gives it structure and meaning.**

---

You can now safely move on to:

* Connecting Neon to ASP.NET Core
* Configuring EF Core
* Running your first migration
