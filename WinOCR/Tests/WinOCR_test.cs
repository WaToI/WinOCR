#if DEBUG
namespace WinOCR {
  using System.IO;
  using System.Reflection;
  using System.Threading.Tasks;
  using System;
  using NUnit.Framework;

  [TestFixture]
  public class WinOCR_test : Nunit_test {

    [TestCase (@"..\tmp\キャプチャ.PNG")]
    //[TestCase (@"")]
    public void TestReadToEndAsync (string path) {
      Console.WriteLine ($@"{new WinOCR().ReadToEndAsync(path).Result}");
    }

    [TestCase (@"..\tmp\キャプチャ.PNG")]
    public void TestReadLine (string path) {
      foreach (var i in new WinOCR ().ReadLine (path)) {
        Console.WriteLine ($@"{i}");
      }
    }

    //[TestCase (@"..\tmp\キャプチャ.PNG")]
    //public async Task TestReadLineAsync (string path) {
    //  await
    //  foreach (var i in new WinOCR ().ReadLineAsync (path)) {
    //    Console.WriteLine ($@"{i}");
    //  }
    //}

    [TestCase (@"..\tmp\キャプチャ.PNG")]
    public void TestCaptureScreenBMP (string path) {
      new WinOCR ().CaptureScreenBMP (path);
    }
  } //class

} //namespace
#endif //DEBUG