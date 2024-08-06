# Farmers API

A simple .NET 9 Minimal API project to manage a list of farmers and their details. This API allows you to perform basic CRUD operations including retrieving, adding, and viewing farmer information.

## Table of Contents
- [Overview](#overview)
- [Features](#features)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Running the API](#running-the-api)
- [API Endpoints](#api-endpoints)
- [Swagger UI](#swagger-ui)
- [License](#license)

## Overview

This project demonstrates the use of .NET 9 Minimal APIs for building a simple RESTful API. The API manages a list of farmers and their respective farm details, including location, products, and contact information.

## Features

- **GET /api/farmers**: Retrieve the list of all farmers.
- **GET /api/farmers/{id}**: Retrieve details of a specific farmer by ID.
- **POST /api/farmers**: Add a new farmer to the list.

## Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- A code editor like [Visual Studio Code](https://code.visualstudio.com/) or [Visual Studio](https://visualstudio.microsoft.com/).

## Installation

1. **Clone the repository**:
    ```bash
    git clone https://github.com/yourusername/farmers-api.git
    cd farmers-api
    ```

2. **Restore dependencies**:
    ```bash
    dotnet restore
    ```

3. **Build the project**:
    ```bash
    dotnet build
    ```

## Running the API

To run the API locally, use the following command:

```bash
dotnet run


## Use case for AMS Integration
**On board Farmer from AMS and send email**:
**Mark Product as InActive(out of stock)**:
**Update Product**:
