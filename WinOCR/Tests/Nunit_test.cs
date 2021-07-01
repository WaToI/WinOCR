#if DEBUG
namespace WinOCR {
  using System.IO;
  using System.Reflection;
  using System;
  using NUnit.Framework;

  [TestFixture]
  public abstract class Nunit_test {
    DirectoryInfo cDi = new DirectoryInfo ("./");
    [OneTimeSetUp]
    public void RunBeforeAnyTests () {
      var dir = Path.GetDirectoryName (typeof (Nunit_test).Assembly.Location);
      Environment.CurrentDirectory = dir;
      cDi = new DirectoryInfo (Environment.CurrentDirectory);
      Console.WriteLine ($@"
setupTests
>----------------------------------------
  pwd: {Environment.CurrentDirectory}
");
    }

    [OneTimeTearDown]
    public void RunAfterAnyTests () {
      Console.WriteLine ($@"
>----------------------------------------
endTests
");
    }

    //public void checkDBisTestDB (DBMasterEdit.Models.DB.Context db) {
    //    if (!db.IsTestDB) {
    //        Assert.Warn ("ｺﾗｰ! ( ｀Д´)ﾉ  本番DB禁止");
    //        throw new Exception ("本番DB禁止");
    //    }
    //}

    [Test]
    public void TestHello () {
      Console.WriteLine ($@"
 pwd {new DirectoryInfo("./").FullName}
 nameof {nameof(TestHello)}
 GetCurrentMethod {MethodBase.GetCurrentMethod()}
 CurrentDir is {Directory.GetCurrentDirectory()}
");
    }

  } //class
}
#endif //DEBUG