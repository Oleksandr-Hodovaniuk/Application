# Coworking

A simple application for booking rooms and desks in coworking spaces.

---

## Requirements

- Git (version 2.30 or later)
- .NET 9.0 SDK
- Angular CLI 20.0.0
- Node.js 22.16.0
- npm 11.14.1
- PostgreSQL 15.x (or later)
- Modern browser (Chrome, Firefox, Edge)

---

## Installation and Launch

### Step 1: Clone the repository

git clone https://github.com/Oleksandr-Hodovaniuk/Application.git  
cd Application

---

### Step 2: Backend setup (.NET 9)

Navigate to the backend folder (Server):  
cd Server

Install dependencies and build the project:  
dotnet restore  
dotnet build

Edit the appsettings.json file to configure the PostgreSQL database connection. Example:  
"ConnectionStrings": {
  "CoWorkingDb": "Host=localhost;Port=5432;Database=coworking_db;Username=your_username;Password=your_password"
}

Open the Package Manager Console, select CoWorking.Infrastructure as the default project, create and apply a migration:
add-migration Ititial
update-database

Navigate to the CoWorking.API folder:
cd CoWorking.API

Run the server: 
dotnet run

---

### Step 3: Frontend setup (Angular)

Navigate to the frontend folder (Front): 
cd ../Front

Install dependencies:  
npm install

Run the frontend:  
ng serve

By default, the application will be available at: http://localhost:4200

---

## Usage

- First, start the backend (dotnet run inside the CoWorking.API folder).
- Then, start the frontend (ng serve inside the Front folder).
- Open http://localhost:4200 in your browser and use the application.

---

## Author

Oleksandr Hodovaniuk — s.godovanuk@gmail.com — https://t.me/Vediciy

---

## License

MIT License