# Virtual File System Simulator

Welcome to the Virtual File System Simulator GitHub repository!

## Overview

The Virtual File System Simulator project emulates a basic file system within a virtual disk represented by a file (`Disk.txt`). This simulator allows users to perform essential file and directory operations similar to those available in a traditional operating system environment.

## Key Features

### Virtual Disk Representation
- The virtual disk is represented by a single file (`Disk.txt`), with a maximum size limit of 1MB.
- This file serves as the storage medium for the simulated file system, encapsulating directories, files, and metadata.

### File System Operations
- **File Creation:** Create new files within the virtual file system.
- **Directory Creation:** Create directories (folders) to organize files hierarchically.
- **File Copying:** Duplicate files within the file system.
- **File Deletion:** Delete files from the virtual disk.
- **Directory Deletion:** Delete directories from the virtual disk.
- **File Renaming:** Rename files and directories.
- **Listing Content:** List the contents of directories.
- **Navigating Directory Structure:** Traverse through directories.

### Console Commands
- **help:** Display information about the commands.
- **cls:** Clear the console.
- **quit:** Exit the console.
- **cd:** Change directory. Usage: `cd [directory_name]`
- **md:** Create directory. Usage: `md [directory_name]`
- **rd:** Remove directory. Usage: `rd [directory_name]`
- **rename:** Rename file or directory. Usage: `rename [old_name] [new_name]`
- **copy:** Copy files. Usage: `copy [source_file] [destination_file]`
- **import:** Import a text file. Usage: `import [file_path]`
- **export:** Export a text file. Usage: `export [file_name] [destination_path]`
- **type:** Display file contents. Usage: `type [file_name]`
- **del:** Delete files. Usage: `del [file_name]`

### Implementation Details
- **Directory Structure:** Uses a simplified directory structure akin to FAT32, with directories containing entries pointing to file metadata (e.g., name, size, location).
- **File Allocation Table (FAT):** A basic implementation of a FAT-like structure manages disk space allocation and file location information.
- **Metadata Management:** Each file and directory is associated with metadata (e.g., name, size, location) stored within the virtual disk.

## Technologies Used
- **C#:** Core programming language used for the project.
- **.NET Framework:** Leveraging .NET for system and file I/O operations.
- **Console Application:** Project implemented as a command-line interface application.
- **Visual Studio:** IDE used for development and debugging.

## Purpose
- Provide a hands-on learning experience in file system concepts and operations.
- Offer a simplified yet functional simulation of a file system, enabling users to understand fundamental file management tasks.

## Contributing

Contributions are welcome! Fork this repository and submit a pull request. For major changes, please open an issue first to discuss what you would like to change.

## Contact

For questions or feedback, reach out via LinkedIn or email.

---

Created by Aalaa Ayman

Thank you for using Virtual File System Simulator!
