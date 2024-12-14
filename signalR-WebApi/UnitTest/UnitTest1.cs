using Model;
using Model.Services;
using Service;

namespace UnitTest;

public class Tests
{
    private User usr;

    [SetUp]
    public void Setup()
    {
        usr = new User();
        usr.Name = "Leandro Cadete";
        usr.Email = "cadetedasilva@gmail.com";
        usr.Pwd = "99MM00123";
        usr.Id = 10;

    }

    [Test]
    public void Test1()
    {
        ILogin login = new Login();
        var result = login.Login(this.usr);
        Assert.That(result.Success, Is.True);
    }

    [Test]
    [TestCase("123456*")]
    [TestCase("123456@")]
    [TestCase("123456#")]
    public void TestPwd_whenItHas6Characters_shouldReturnTrue(string strPwd) {
        User usr = new User();
        usr.Pwd = strPwd;
        var ret = usr.Validate();
        Assert.That(ret, Is.True);
    }

    [Test]
    public void GeterateToken_shouldGenerateToken_returnToken() {
        var mockLogin = new Moq.Mock<ILogin>();
        mockLogin.Setup(l => l.Login(this.usr)).Returns(new Result() { Messages = new [] {"moq-message" }, Success = true, Value = "Token-moq"});

        bool r = usr.ValidateToken(mockLogin.Object);
        Assert.That(r, Is.True);
    }
}