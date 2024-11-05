#!/bin/bash

# Base directory for the CollectionManagementService
BASE_DIR="CollectionManagementService"

# Create the main service directory
mkdir -p "$BASE_DIR"

# Create subdirectories
mkdir -p "$BASE_DIR/Controllers"
mkdir -p "$BASE_DIR/DTOs"
mkdir -p "$BASE_DIR/Models"
mkdir -p "$BASE_DIR/Repositories"
mkdir -p "$BASE_DIR/Services"
mkdir -p "$BASE_DIR/Data"

# Create main project files
touch "$BASE_DIR/CollectionManagementService.csproj"
touch "$BASE_DIR/Dockerfile"
touch "$BASE_DIR/appsettings.json"

# Create individual class files
touch "$BASE_DIR/Controllers/CollectionController.cs"
touch "$BASE_DIR/DTOs/CollectionDTO.cs"
touch "$BASE_DIR/Models/Collection.cs"
touch "$BASE_DIR/Repositories/ICollectionRepository.cs"
touch "$BASE_DIR/Repositories/CollectionRepository.cs"
touch "$BASE_DIR/Services/ICollectionService.cs"
touch "$BASE_DIR/Services/CollectionService.cs"
touch "$BASE_DIR/Data/DataContext.cs"

echo "CollectionManagementService structure created successfully."
