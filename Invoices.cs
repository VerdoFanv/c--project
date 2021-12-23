using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uts {
  class Invoices {
    public void create(string text, string id, string path = @"transactions\") {
      try {
        if (!Directory.Exists(path))
          Directory.CreateDirectory(path);

        string fullPath = Path.Combine(path);
        string fileName = $"transaction_{id}_{DateTime.Now.ToString("ddMMMyyyyHHmmss")}.txt";
        fullPath += fileName;

        File.WriteAllText(fullPath, text);
      } catch (Exception error) {
        Console.WriteLine(error.Message);
      }
    }

    public string idGenerator(int saltRound = 10) {
      var random = new Random();
      var bytes = new Byte[saltRound];
      random.NextBytes(bytes);

      var hexArray = Array.ConvertAll(bytes, x => x.ToString("X2"));
      var hexStr = String.Concat(hexArray);
      return hexStr;
    }
  }
}
