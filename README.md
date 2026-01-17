# 🎨 PortfolioCV - Dynamic Portfolio & CV Platform

A modern, full-featured portfolio management system built with extensive .NET 8 and React integration.
It features a public-facing dynamic portfolio site and a powerful, separate Request-based Admin Panel built with **Refine**.

## 🏗 Architecture

*   **Public Frontend:** ASP.NET Core MVC (Dynamic Content, SEO Optimized)
*   **Admin Panel:** React 18, Vite, Refine Framework, Ant Design
*   **Backend API:** ASP.NET Core Web API (JWT Authentication)
*   **Database:** MSSQL (Entity Framework Core)

## ✨ Key Features

### 🔐 Admin Panel (React)
*   **Smart Initialization:** Auto-detects fresh installations and redirects to a setup wizard to create the first admin account.
*   **Modern UI:** Built with Ant Design & Refine.
*   **Secure Auth:** JWT-based authentication with secure cookie handling.
*   **Advanced CRUD:** Smart tables with sorting, filtering, and searching.
*   **Dark/Light Mode:** Full theme support syncing across subdomains.
*   **Multilingual:** TR/EN support with persistent preferences.
*   **IIS Compatible:** Custom **Method Override** support to bypass WebDAV/Firewall restrictions on `PUT/DELETE` requests.

### 🌐 Public Site (MVC)
*   **Dynamic Sections:** All content (Skills, Projects, Experience) is manageable via Admin.
*   **Responsive Design:** Mobile-first approach.
*   **Fast Performance:** Optimized assets and database queries.
*   **Contact Form:** Integrated messaging system directly to Admin Panel.

---

## 🚀 Getting Started

### Prerequisites
*   .NET 8.0 SDK
*   Node.js (v18+)
*   SQL Server

### 1️⃣ Backend Setup (API & Public Site)
1.  Clone the repository.
2.  Update `appsettings.json` with your SQL Connection String.
3.  Apply Migrations:
    ```bash
    dotnet ef database update
    ```
    *This will create the database schema. No initial data is seeded automatically for security reasons.*

4.  Run the application:
    ```bash
    dotnet run
    ```
    (Runs on `https://localhost:7150` by default).

### 2️⃣ Admin Panel Setup
1.  Navigate to the admin folder:
    ```bash
    cd admin-panel
    ```
2.  Install dependencies:
    ```bash
    npm install
    ```
3.  Configure Environment:
    Create `.env` file:
    ```env
    VITE_API_URL=https://localhost:7150
    ```
4.  Start Development Server:
    ```bash
    npm start
    ```

### 3️⃣ First Run & Initialization
When you launch the Admin Panel for the first time:
1.  The system checks if any admin user exists in the database.
2.  If the database is empty (fresh install), you will be automatically redirected to the **/initialize** page.
3.  Fill in the form to create your **Master Admin** account.
4.  Once created, you will be redirected to the Login page to sign in with your new credentials.

---

## 📦 Deployment (IIS)

This project is optimized for IIS deployment.

### Backend Publish
1.  Run `dotnet publish -c Release`.
2.  Upload files to your server (e.g., `www.yourdomain.com` or `api.yourdomain.com`).
3.  **Important:** Ensure `web.config` is present to handle IIS modules.

### Admin Panel Publish
1.  Run `npm run build` inside `admin-panel`.
2.  Upload `dist` folder contents to your admin subdomain or subfolder (e.g., `dashboard.yourdomain.com`).

### ⚠️ Troubleshooting 405/403 Errors on IIS
If you encounter `403 Forbidden` or `405 Method Not Allowed` on **DELETE/PUT** requests:
*   This project includes a built-in **Method Override** feature.
*   The Admin Panel automatically converts `DELETE` and `PUT` requests to `POST` requests with a special header (`X-HTTP-Method-Override`).
*   Ensure the Backend is published correctly, as `Program.cs` contains the middleware to handle this override.

---

## 🛠 Tech Stack
*   **Core:** .NET 8, C#
*   **Frontend:** HTML5, CSS3, JavaScript
*   **Admin:** React, TypeScript, Vite, Refine
*   **Data:** EF Core, SQL Server

## 📄 License
MIT License.
