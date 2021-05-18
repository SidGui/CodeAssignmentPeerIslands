using CodeAssignmentPeerIslands.Domain;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CodeAssignmentPeerIslands.Infra
{
    public class ReadJsonFile
    {
        public async Task<SQL> ConvertJsonToObject(string filePath)
        {
            SQL parsedFile = null;
            try
            {
                var jsonFile = await File.ReadAllTextAsync(filePath);
                parsedFile = JsonConvert.DeserializeObject<SQL>(jsonFile);
            } catch(Exception error)
            {
                throw new Exception($"[ERROR] => Trying to read the file, something wrong happens. \n Original error: ${error.Message}");
            }        
            return parsedFile;
        }
    }
}
