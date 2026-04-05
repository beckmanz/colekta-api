namespace colekta_api.Models.RequestDtos;
/// <summary>
/// DTO de requisição para acesso (Login) de um usuário.
/// </summary>
/// <param name="Email">
/// E-mail do usuário (deve ser válido).
/// <para>Exemplo: <c>fulano@email.com</c></para>
/// </param>
/// <param name="Senha">
/// Senha em texto puro.
/// <para>Exemplo: <c>SenhaForte@123</c></para>
/// </param>
public record LoginDto(string Email, string Senha);