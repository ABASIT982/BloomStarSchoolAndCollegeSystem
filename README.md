# BloomStar School & College Management System

Full-stack ASP.NET Core 8 application:
- School (Nursery–10th) and College management
- Admissions, Fees & Scholarships
- Faculty/Teacher management & Salary
- RBAC roles: Admin, Accountant, Admission Officer

## How to run
1. Clone this repo
2. Set your own SQL Server connection string in `appsettings.json`
3. Run `Update-Database`
4. Press F5 to start

5. School Students Module

Added Students → School page with Index file

Allows searching & filtering by Grade/Section

Shows list of School Students from database

Sidebar Updates

Added “School Students” option to the left sidebar for quick access

Database Changes

Added new column SectionName in the Students table

Updated EF Core model to match database changes

Bug Fixes

Fixed navigation issues between Admin and Students sections

Fixed missing layout reference and @click error
