# OneDrive2Podcast
A simple executable for turning a folder of media files into a podcast feed, hosted on OneDrive.

## Get the Executable
- Downloa

## How to Use
1. Go to https://onedrive.com
1. Navigate to a public folder where you want to store your media files (i.e. '/podcast/public')
1. Take a look at the URL in the address bar. For example, it should look something like `https://onedrive.live.com/?id=A1B2C3D4E5F6G7H8%21Z0Y1X2&cid=A1B2C3D4E5F6G7H8`.
1. Copy the value of the `id` parameter. In this example, that would be `A1B2C3D4E5F6G7H8%21Z0Y1X2`.
1. Open `OneDrive2Podcast.exe.config`.
1. Replace `[FOLDER_ITEM_ID]` with the copied value.
