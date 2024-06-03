using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Curso.Dapper.Api.ValueObjects;

public class Email
{
    public string Endereco { get; private set; }

    public Email(string endereco)
    {
        if (string.IsNullOrWhiteSpace(endereco))
        {
            throw new ArgumentException("Endereço de e-mail inválido");
        }
        Endereco = endereco;
    }

    public override string ToString()
    {
        return Endereco;
    }

    // override object.Equals
    public override bool Equals(object obj)
    {

        if (obj is Email email)
        {
            return Endereco == email.Endereco;
        }

        return false;
    }
    public override int GetHashCode()
    {
        return Endereco.GetHashCode();
    }

    public static implicit operator Email(string endereco)
    {
        return new Email(endereco);
    }

    public static implicit operator string(Email email)
    {
        return email.Endereco;
    }

    public static bool operator ==(Email left, Email right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Email left, Email right)
    {
        return !(left == right);
    }

    public bool EhValido()
    {
        return new EmailAddressAttribute().IsValid(Endereco);
    }

    public static bool IsValid(string email)
       => Regex.IsMatch(email, @"^[a-z0-9.]+@[a-z0-9]+\.[a-z]+(\.[a-z]+)?$", RegexOptions.IgnoreCase);
}
