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

✨ New Features Added

🔹 Admissions

Add New Admission

Supports School and College students.

Dropdowns for Class/Year and Section/Department.

Photo upload with preview before saving.

Reset button (clears form + preview).

Fully responsive Tailwind UI with professional design.

Update Admission

Admin can update existing student details (Name, Parent Info, Contact, Address, Class/Year, Section/Department, Photo).

Smooth modal-based editing with form validation.

Manage Admissions

Filter students by Type (School/College), Class/Year, Section/Department, or search by Name/Parent.

Auto-refresh filters (no need to click Apply).

Students displayed in a professional table format with:

Circle photo

All details in row format

Action buttons (Update / Delete)

Delete removes record from database with confirmation + success message.

Update opens modal with editable details.

🔹 Teachers

Add New Teacher

Form for adding teacher details (Name, Subject, Contact, Address, Salary, etc.).

Photo upload with preview before saving.

Reset button included for convenience.

Teacher Listing

Displays all teachers in a row format table.

Circle photo, teacher info, subject, contact, salary details shown.

Options for Update and Delete teacher records.

Fully responsive UI for professional use.

Teacher Salaries

Admin can manage and view teacher salaries.

Shows list of all teachers with their current salary status.

Option to mark salary as Paid / Unpaid.

Salary details updated in database.

Success messages after payment action.

Pay Salaries Page

Separate page for handling salary payments.

Teachers listed with Salary Amount + Payment Button.

Once paid, salary status updates immediately with confirmation message.

Fixed navigation issues between Admin and Students sections

Fixed missing layout reference and @click error
Pages Updates
add pages for add student delete studetn adn update studnet for school or college 
