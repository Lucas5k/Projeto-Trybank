using Xunit;
using FluentAssertions;
using trybank;
using System;

namespace trybank.Test;

public class TestFourthReq
{
    [Theory(DisplayName = "Deve transefir um valor com uma conta logada")]
    [InlineData(80, 100)]
    public void TestTransferSucess(int balance, int value)
    {
        Trybank instance = new();

        instance.Transfer(12, 1200, balance);

        instance.Bank[0, 3].Should().Be(value);
    }

    [Theory(DisplayName = "Deve lançar uma exceção de usuário não logado")]
    [InlineData(80)]
    public void TestTransferWithoutLogin(int value)
    {
        Trybank instance = new();

        instance.Transfer(12, 1200, value);
        Action act = () => instance.Transfer(12, 1200, value);

        act.Should().Throw<AccessViolationException>().WithMessage("Usuário não está logado");
    }

    [Theory(DisplayName = "Deve lançar uma exceção de usuário não logado")]
    [InlineData(0, 0)]
    public void TestTransferWithoutBalance(int balance, int value)
    {
        Trybank instance = new();

        instance.Transfer(12, 1200, value);

        Action act = () => instance.Transfer(12, 1200, balance);

        act.Should().Throw<InvalidOperationException>().WithMessage("Saldo insuficiente");
    }
}
