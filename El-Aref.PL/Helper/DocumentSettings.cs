namespace El_Aref.PL.Helper
{
    public static class DocumentSettings 
    {
        // 1. UpLoad
        public static string UpLoadFile(IFormFile file , string foldername)
        {
            // 1. Get Folder Location
            //string folderpath = "D:\\ASP.NET\\projects\\P02\\El-Aref.PL\\wwwroot\\Files\\"+ foldername;

            //var folderpath = Directory.GetCurrentDirectory() + "\\wwwroot\\Files\\"+foldername;

            var folderpath = Path.Combine(Directory.GetCurrentDirectory() , @"wwwroot\Files" , foldername);

            // 2. Get File Name And It Uniqe
            var fileName = $"{Guid.NewGuid()}{file.FileName}";

            // File Path
            var filePath = Path.Combine(folderpath, fileName);
            var filestream = new FileStream(filePath, FileMode.Create);
            file.CopyTo(filestream);
            
            return fileName;

        }
        // 2. Delete
        public static void DeleteFile (string filename , string foldername)
        {
            var filepath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files", foldername,filename);
            if (File.Exists(filepath))
                File.Delete(filepath);
        }










    }
}
