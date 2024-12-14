using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System;
using System.IO;
using System.Security.Principal;


public interface ICloudinaryService
{
    string UploadImage(string filePath);
}
public class CloudinaryService : ICloudinaryService
{
    private readonly Cloudinary _cloudinary;
    private readonly string _cloudName;
    private readonly string _apiKey;
    private readonly string _apiSecret;

    public CloudinaryService(string cloudName, string apiKey, string apiSecret)
    {
        _cloudName = cloudName;
        _apiKey = apiKey;
        _apiSecret = apiSecret;

        _cloudinary = new Cloudinary(new Account(cloudName, apiKey, apiSecret));
    }
    //public CloudinaryService(string cloudName, string apiKey, string apiSecret)
    //{
    //    _cloudinary = new Cloudinary(new Account(cloudName, apiKey, apiSecret));
    //}
    private string GenerateRandomString(int length)
    {
        // Tập hợp các ký tự thường và số
        const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
        Random random = new Random();
        char[] result = new char[length];

        for (int i = 0; i < length; i++)
        {
            // Lấy ngẫu nhiên một ký tự từ tập hợp
            result[i] = chars[random.Next(chars.Length)];
        }

        return new string(result);
    }

    private string AssetNameGenerator()
    {
        var resources = _cloudinary.ListResources(new ListResourcesParams()
        {
            Type = "upload"
            //= "RecipeHaven/"
        }).Resources;


        string assetName = GenerateRandomString(10);
        bool flag = false;

        for (; ; )
        {
            if (assetName == null) break;
            foreach (var resource in resources)
            {
                var existedName = resource.PublicId.Split('/').Last();
                if (assetName == existedName)
                {
                    flag = true;
                }
            }
            if (!flag) {  break; }
            if (flag)
            {
                 assetName = GenerateRandomString(10);
            }            
        }
        return assetName;
    }

    public string UploadImage(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("File doesn't exist", filePath);
        }

        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(filePath),
            PublicId = $"RecipeHaven/{AssetNameGenerator()}",
            Folder = "RecipeHaven",
            Overwrite = true
        };

        var uploadResult = _cloudinary.Upload(uploadParams);

        if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
        {
            return uploadResult.SecureUrl.ToString();
        }
        else
        {
            throw new Exception($"Upload Fail: {uploadResult.Error?.Message}");
        }
    }
}
