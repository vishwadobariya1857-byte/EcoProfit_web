# EcoProfit – Smart Waste-to-Profit Advisor

ASP.NET Core MVC academic mini-project. Supports **Tailoring** and **Plastic Bottle Manufacturing** businesses.

## Requirements
- .NET 8 SDK
- MySQL Server 8.x (running)
- Visual Studio 2022 / VS Code
- Internet for initial NuGet restore and Bootstrap CDN

## Quick start
1. Create a MySQL database: `CREATE DATABASE ecoprofit_db;` (see `database.sql`).
2. Open `appsettings.json` and replace `YOUR_MYSQL_PASSWORD` with your MySQL root password. Prefer .NET user-secrets for non-demo deployments.
3. Open a terminal inside `EcoProfit`.
4. Run `dotnet restore`.
5. Install EF CLI if missing: `dotnet tool install --global dotnet-ef --version 8.0.8`.
6. Generate migration: `dotnet ef migrations add InitialCreate`.
7. Apply schema: `dotnet ef database update`.
8. Start: `dotnet run`.
9. Open the localhost URL printed in the terminal. Register a user, then create entries.

> **Do not import the manual table schema and also run EF migrations**. The provided SQL file creates only the empty database; EF migrations create the tables.

## Modules
- Account: register/login/logout; secure password hashing; session authentication for class demo
- Dashboard: totals and high-priority entries
- Waste: create/read/update/delete with model validation and ownership checks
- Smart analysis: rule-based suggestions and quantity-based priority
- Reports: totals by business/material and CSV export

## Demo example
Business: Plastic Bottle Manufacturing; Material: PET plastic scrap; Quantity: 25; Rate: 15; Estimated value: ₹375; Priority: Medium.

## Notes
- Recovery value = quantity (kg) × manually entered estimated rate (₹/kg).
- Priority: Low <15 kg, Medium 15–49.99 kg, High ≥50 kg.
- This is **rule-based decision support, not AI/ML**. Estimated value is not actual profit.
- This is a classroom demo, not production authentication. For production use ASP.NET Core Identity, account lockout, persistent session storage, stronger security controls, and HTTPS.
- The CSV export opens in Excel. Exported content should be treated as untrusted input when opened in spreadsheet software.
- No PDF generation is included; poster is a separate deliverable.

## GitHub
Upload this entire `EcoProfit` folder (exclude `bin`, `obj`, `.vs`, user-secrets and real credentials) to your repository. A `.gitignore` is included.
