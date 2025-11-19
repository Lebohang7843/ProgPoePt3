# Claims Management System - PROG6212 POE Part 3

A comprehensive ASP.NET Core web application for managing lecturer contract claims, built as part of the PROG6212 Programming POE.

![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-6.0-purple)
![Bootstrap](https://img.shields.io/badge/Bootstrap-5.0-blue)
![C#](https://img.shields.io/badge/C%23-10.0-green)
![SQL Server](https://img.shields.io/badge/SQL_Server-Express-orange)


Presentation: [Lebohang selematsela prog poe pt3 Contract_Claim_System_Modern_Styled.pptx](https://github.com/user-attachments/files/23638521/Lebohang.selematsela.prog.poe.pt3.Contract_Claim_System_Modern_Styled.pptx)

## 📋 Project Overview

This Claims Management System is a multi-role web application designed to streamline the process of submitting, reviewing, approving, and settling lecturer contract claims. The system follows a structured workflow with four distinct user roles, each with specific responsibilities and access levels.

## 🎯 Features

### 👨‍🏫 Lecturer Portal
- **Claim Submission**: Submit new claims with automated calculations
- **Real-time Validation**: Smart form validation with AI-powered suggestions
- **Document Upload**: Support for PDF, DOCX, and XLSX files
- **Claim Tracking**: Monitor claim status through the approval workflow
- **Auto-calculation**: Automatic total amount calculation based on hours and rate

### 👨‍💼 Coordinator Portal  
- **Claim Verification**: Review and validate submitted claims
- **Quality Control**: Ensure claims meet institutional requirements
- **Progress Tracking**: Monitor claims through the approval pipeline
- **Document Review**: Verify supporting documentation

### 👨‍💻 Manager Portal
- **Final Approval**: Authorize claims for payment processing
- **Financial Oversight**: Review claim amounts and justifications
- **Workflow Management**: Oversee the entire claims process
- **Reporting Access**: Generate departmental reports

### 👨‍💼 HR Portal
- **Payment Processing**: Settle approved claims with payment references
- **Settlement Queue**: Manage claims ready for payment
- **Payment Tracking**: Maintain settlement records and references
- **Financial Reporting**: Generate payment reports and analytics
- **Historical Data**: Access settled claims history

## 🏗️ System Architecture

### Technology Stack
- **Backend**: ASP.NET Core 6.0 MVC
- **Frontend**: Bootstrap 5.3, Font Awesome 6.0
- **Database**: Entity Framework Core with SQL Server
- **Validation**: Client-side JavaScript with server-side ModelState validation
- **Security**: Anti-forgery tokens, role-based access control
