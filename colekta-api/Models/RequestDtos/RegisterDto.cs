namespace colekta_api.Models.RequestDtos;

/// <summary>
/// DTO de requisição para registro (criação) de um novo usuário.
/// </summary>
/// <param name="NomeCompleto">
/// Nome completo do usuário.
/// <para>Exemplo: <c>Fulano de Tal</c></para>
/// </param>
/// <param name="Email">
/// E-mail do usuário (deve ser válido e ainda não cadastrado).
/// <para>Exemplo: <c>fulano@email.com</c></para>
/// </param>
/// <param name="Senha">
/// Senha em texto puro (será validada pelas regras do ASP.NET Identity).
/// <para>Exemplo: <c>SenhaForte@123</c></para>
/// </param>
public record RegisterDto(string NomeCompleto, string Email, string Senha);