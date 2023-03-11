using Xunit;
using FluentAssertions;
using trybank;
using System;

namespace trybank.Test;

public class TestSecondReq
{
    [Theory(DisplayName = "Deve logar em uma conta!")]
    [InlineData(55, 1550, 0)]
    public void TestLoginSucess(int number, int agency, int pass)
    {
        Trybank instance = new();

        instance.Login(number, agency, pass);

        instance.Logged.Should().Be(true);
    }

    [Theory(DisplayName = "Deve retornar exceção ao tentar logar em conta já logada")]
    [InlineData(60, 1200, 0)]
    public void TestLoginExceptionLogged(int number, int agency, int pass)
    {
        Trybank instance = new();

        instance.Login(number, agency, pass);
        Action act = () => instance.Login(number, agency, pass);

        act.Should().Throw<AccessViolationException>().WithMessage("Usuário já está logado");
    }

    [Theory(DisplayName = "Deve retornar exceção ao errar a senha")]
    [InlineData(700, 70, 1234)]
    public void TestLoginExceptionWrongPass(int number, int agency, int pass)
    {
        Trybank instance = new();

        instance.Login(number, agency, pass);
        Action act = () => instance.Login(number, agency, pass);

        act.Should().Throw<ArgumentException>().WithMessage("Senha incorreta");
    }

    [Theory(DisplayName = "Deve retornar exceção ao digitar conta que não existe")]
    [InlineData(70, 800, 141412)]
    public void TestLoginExceptionNotFounded(int number, int agency, int pass)
    {
        Trybank instance = new();

        instance.Login(number, agency, pass);
        Action act = () => instance.Login(number, agency, pass);

        act.Should().Throw<ArgumentException>().WithMessage("Agência + Conta não encontrada");
    }

    [Theory(DisplayName = "Deve sair de uma conta!")]
    [InlineData(100, 5000, 8000)]
    public void TestLogoutSucess(int number, int agency, int pass)
    {
        Trybank instance = new();

        instance.Login(number, agency, pass);
        instance.Logout();

        instance.Logged.Should().Be(false);
    }

    [Theory(DisplayName = "Deve retornar exceção ao sair quando não está logado")]
    [InlineData(180, 40000, 1110)]
    public void TestLogoutExceptionNotLogged(int number, int agency, int pass)
    {
        Trybank instance = new();

        instance.Login(number, agency, pass);
        Action act = () => instance.Login(number, agency, pass);

        instance.Logout();

        act.Should().Throw<AccessViolationException>().WithMessage("Usuário não está logad");
    }

}
