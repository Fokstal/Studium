using api.Helpers;
using api.Helpers.Constants;

namespace api.Repositories
{
    public static class PictureRepository
    {
        private static readonly string picturesFolderPath = "./wwwroot/pictures/";
        private static readonly string defaultPersonPicturesFolderPath = "./AppData/Pictures/PersonDefault";

        private static IFormFile GetPicture(string folderName, string fileName)
        {
            string pathToFileName = Path.Combine($"{folderName}/{fileName}");

            FileStream fileStream = File.OpenRead(pathToFileName);

            IFormFile picture = new FormFile(fileStream, 0, fileStream.Length, null!, pathToFileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/jpeg"
            };

            return picture;
        }

        private static async Task<string> UploadPicture(PictureFolders.PictureFolderEntity pictureFolder, IFormFile picture)
        {
            string pictureGuidName = Guid.NewGuid().ToString();
            string pictureExtension = Path.GetExtension(picture.FileName);

            using (FileStream fileStream = new(Path.Combine($"{picturesFolderPath}/{pictureFolder.Path}/{pictureGuidName + pictureExtension}"), FileMode.Create))
            {
                await picture.CopyToAsync(fileStream);
            }

            string passportScanFileName = pictureGuidName + pictureExtension;

            return passportScanFileName;
        }

        public static async Task<string> UploadPassportScanAsync(IFormFile passportScan)
        {
            if (passportScan is null) throw new Exception("Passport.Scan is null!");

            string passportScanFileName = await UploadPicture(PictureFolders.Passport, passportScan);

            return passportScanFileName;
        }

        public static async Task<string> UploadPersonAvatarAsync(IFormFile? personAvatar, int personSex = 0)
        {
            if (personAvatar is null)
            {
                Random random = new();
                int randomMaxValue = 9;

                if (personSex == 1) randomMaxValue = 8;

                string defaultAvatarName = PersonHelper.SexStringByInt(personSex) + "-" + random.Next(1, randomMaxValue) + ".png";

                personAvatar = GetPicture(defaultPersonPicturesFolderPath, defaultAvatarName);
            }

            string personAvatarFileName = await UploadPicture(PictureFolders.Person, personAvatar);

            return personAvatarFileName;
        }

        public static void RemovePicture(PictureFolders.PictureFolderEntity pictureFolder, string pictureName)
        {
            File.Delete($"{picturesFolderPath}/{pictureFolder.Path}/{pictureName}");
        }
    }
}