Proof of concept for the product catalog image resizer.
======================================================

Here is how it works:

All files uploaded to a Azure storage blob container “source” directory (and its subdirectories) are processed
-	If the file is an image file (with extension of jpg, png, gif or bmp), it is resized to a max width and max height (keeping the same aspect ratio and file type) and copied into a “destination” directory
-	If the file is not an image, it is copied as is into the “destination directory

The code is fairly well commented and short, so you can see the code for details.

Important note
-	The Azure Storage Account needs to be of StorageV2 type.

To Do:
-	Need to add the connection strings to connecting to Azure storage (so far my testing has been local on the storage emulator)

