Product catalog image resizer.
==============================

Here is how it works:

All files uploaded to a Azure storage blob container 'source' directory (and its subdirectories) are processed
-	If the file is an image file (with extension of jpg, png, gif or bmp), it is resized to a max width and max height (keeping the same aspect ratio and file type) and copied into a 'destination' directory
-	If the file is not an image, it is copied as is into the 'destination' directory without any changes made.

The code is fairly well commented and short, so you can see the code for details.

**Important notes**
-	The Azure Storage Account needs to be of StorageV2 type.
- A 'local.settings.json' file is needed with the following info (it is not saved due to being listed in the .gitignore file):
```
  {
  "IsEncrypted": false,
  "Values": {
    "AzureStorageConnection": "DefaultEndpointsProtocol=https;AccountName=<Blog-Container-Name>;AccountKey=<key-info>;EndpointSuffix=core.windows.net",
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "AzureWebJobsDashboard": "UseDevelopmentStorage=true"
  }
```
- The connection string info for "AzureStorageConnection" will need to be copied from the Azure Portal "Access keys" menu.
