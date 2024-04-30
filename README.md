# Virtual File System Simulator
Welcome to GitHub repository!

# Overview:
The Virtual File System Simulator project is designed to emulate a basic file system within a virtual disk represented by a file (Disk.txt).This simulator allows users to perform essential file and directory operations similar to those available in a traditional operating system environment.

# Key Features:
### 1. Virtual Disk Representation:
  * The virtual disk is represented by a single file (Disk.txt), with a maximum size limit of 1MB.
  * The file serves as the storage medium for the simulated file system, encapsulating
  directories, files, and metadata.

### 2. File System Operations:
  * **File Creation:** Users can create new files within the virtual file system.
  * **Directory Creation:** Directories (folders) can be created to organize files hierarchically.
  * **File Copying:** Files can be duplicated within the file system.
  * **File Deletion:** Users can delete files from the virtual disk.
  * **Directory Deletion:** Users can delete directories from the virtual disk.
  * **File Renaming:** The system supports renaming of files and directories.
  * **Listing Content:** Users can list the contents of directories.
  * **Navigating Directory Structure:** Users can traverse through directories.
    
### 3. Implementation Details:
  * **Directory Structure:** The file system uses a simplified directory structure akin to FAT32, with directories containing entries pointing to file metadata (e.g., name, size, location).
  * **File Allocation Table (FAT):** A basic implementation of a FAT-like structure manages disk space allocation and file location information.
  * **Metadata Management:** Each file and directory is associated with metadata (e.g., name, size, location) stored within the virtual disk.


# Technologies Used:
* **C#**: Core programming language used for the project.
* **.NET Framework**: Leveraging .NET for system and file I/O operations.
* **Console Application**: Project implemented as a command-line interface application.
* **Visual Studio**: IDE used for development and debugging.

# Purpose:
* The project aims to provide a hands-on learning experience in file system concepts and operations.
* It offers a simplified yet functional simulation of a file system, enabling users to understand fundamental file management tasks.
  
# Additional Information
For any questions, issues, or suggestions, please feel free to open an issue on GitHub.

Thank you for using Virtual File System Simulator!
