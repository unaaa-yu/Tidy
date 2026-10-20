Tidy
│
├── backend
│   ├── AI
│   ├── Models
│   ├── Services
│   └── Utils
│
├── data
│   ├── Database
│   ├── Entities
│   └── Repositories
│
├── docs
│   ├── architecture.md
│   ├── commands.md
│   └── database.md
│
├── frontend
│   ├── cli
│   └── gui
│
├── Program.cs
├── README.md
└── Tidy.csproj

# Tidy Architecture

## Overview

Tidy is an AI-powered tool that helps users clean and organize files by:

- Scanning folders
- Detecting duplicate files and categorizing them using AI
- Generating a preview of proposed changes and let users decide whether to apply them
- Supporting Undo and Redo
- Providing both CLI and GUI interfaces

The application follows a layered architecture:

```text
Frontend
    ↓
Backend
    ↓
Data (SQLite)
```

---

## Project Structure

```text
Tidy

├── Backend
├── Data
├── Docs
├── Frontend
├── Program.cs
└── Tidy.csproj
```

---

## Frontend Layer

The Frontend layer is responsible for interacting with the user.

### Responsibilities

- Accept user commands
- Display scan results
- Display folder previews
- Launch GUI
- Display history
- Trigger undo and redo operations

### Structure

```text
Frontend

├── Cli
└── Gui
```

### CLI Commands

```text
scan <path>
preview
apply
undo
redo
history
open-ui
help
exit
```

### GUI Features

```text
Folder selection
Preview
Apply changes
Undo
Redo
```

Frontend should never directly access the database.

---

## Backend Layer

The Backend layer contains business logic.

### Responsibilities

- Scan directories
- Calculate file hashes
- Detect duplicates
- Categorize files
- Generate preview trees
- Handle undo and redo

### Structure

```text
Backend

├── AI
├── Models
├── Services
└── Utils
```

### Models

```text
FileItem
FileOperation
ScanSession
PreviewNode
DuplicateRecords
```

### Services

```text
 FolderScannerService
 DuplicateDetectorService
 AIService
 PreviewService
 OperationService
 UndoRedoService
```

#### FolderScannerService

Scans folders and gathers metadata.

Produces:

```text
File name
Path
Extension
Size
Hash
```

---

#### DuplicateDetectorService

Uses hashes to identify duplicate files.

Produces:

```text
Duplicate groups
Deletion recommendations
```

---

#### AIService

Categorizes files into logical groups.

Example:

```text
Resume.pdf -> Career
Invoice.pdf -> Finance
IMG001.jpg -> Photos
```

---

#### PreviewService

Builds proposed folder structures before any changes are applied.

Example:

```text
Career
├── Resume.pdf

Finance
├── Invoice.pdf
```

---

#### OperationService

Executes file operations.

Supported operations:

```text
Move
Rename
Archive Duplicate
```

---

#### UndoRedoService

Maintains operation history.

Supports:

```text
Undo
Redo
History Retrieval
```

---

## AI Module

The AI module is responsible for file categorization.

### Structure

```text
Backend

└── AI
    ├── Prompts
    ├── Providers
    └── Classifiers
```

### Workflow

Input:

```text
Resume.pdf
SchoolTranscript.pdf
Invoice_2026.pdf
```

Output:

```text
Career
Education
Finance
```

The AI only provides suggestions.

Actual file changes require user approval.

---

## Data Layer

The Data layer handles persistence.

### Responsibilities

- Save scans
- Save operation history
- Store undo/redo state
- Load previous sessions

### Structure

```text
Data

├── Database
├── Entities
└── Repositories
```

---

## Database

Technology:

```text
SQLite
Entity Framework Core
```

Database file:

```text
Tidy.db
```

---

## Database Tables

### ScanSessions

Stores scan history.

```text
Id
FolderPath
ScanTime
FilesScanned
```

---

### Operations

Stores executed actions.

```text
Id
Type
SourcePath
TargetPath
Status
CreatedAt
```

Status values:

```text
Pending
Applied
Undone
```

---

### DuplicateRecords

Stores duplicate information.

```text
Id
Hash
OriginalPath
DuplicatePath
```

---

## Undo / Redo

All changes are represented as operations.

Example:

```text
Move

From:
Downloads\Resume.pdf

To:
Career\Resume.pdf
```

Operation is written to the database.

### Undo

```text
Career\Resume.pdf
↓
Downloads\Resume.pdf
```

Status:

```text
Undone
```

### Redo

```text
Downloads\Resume.pdf
↓
Career\Resume.pdf
```

Status:

```text
Applied
```

---

## Application Flow

### Scan

```text
User
 ↓
scan C:\Folder
 ↓
FolderScannerService
 ↓
DuplicateDetectorService
 ↓
AIClassificationService
 ↓
PreviewBuilderService
```

---

### Preview

```text
User
 ↓
preview
 ↓
PreviewBuilderService
 ↓
CLI Tree View
```

or

```text
User
 ↓
open-ui
 ↓
GUI Preview Window
```

---

### Apply Changes

```text
User
 ↓
apply
 ↓
OperationService
 ↓
Database
 ↓
File System
```

---

### Undo

```text
User
 ↓
undo
 ↓
UndoRedoService
 ↓
Database
 ↓
File System
```

## Design Principles

- User always approves changes before execution.
- AI provides suggestions only.
- Every operation is reversible.
- Frontend never directly accesses SQLite.
- Business logic remains independent from UI.
- CLI and GUI share the same backend services.