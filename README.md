# OneDrive2Podcast
A simple executable for turning a folder of media files into a podcast feed, hosted on OneDrive.

## Get the Executable
- Download the source and compile it.
- (We will provide an executable as this project matures.)

## How to Use
1. Create a folder in OneDrive where you would like to store your podcast files (i.e. 'public/podcast')
1. Place all your podcast files in the folder
2. Place the executable and the config file into the folder (`OneDrive2Podcast.exe`, `OneDrive2Podcast.exe.config`)
1. Go to https://onedrive.com
1. Navigate to the podcast folder you created
1. Take a look at the URL in the address bar. For example, it should look something like `https://onedrive.live.com/?id=A1B2C3D4E5F6G7H8%21Z0Y1X2&cid=A1B2C3D4E5F6G7H8`
1. Copy the value of the `id` parameter. In this example, that would be `A1B2C3D4E5F6G7H8%21Z0Y1X2`.
1. Open the config file
1. Replace `[FOLDER_ITEM_ID]` with the copied value and then save
1. Run the executable by double clicking it
1. You now have a generated RSS feed!
2. Open the RSS file 
3. Your RSS URL will be in '//rss/channel/link' and will look something like https://api.onedrive.com/v1.0/drives/0123456ABCDEFGHI/root:/Public/Podcast/rss.xml:/content
1. Copy and paste the URL into your favorite podcatcher
1. Enjoy!
