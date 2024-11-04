# DemoJwtRs256

Este projeto é uma API em ASP.NET Core que utiliza JWT (JSON Web Tokens) com assinatura RS256 para autenticação de usuários. Ele inclui funcionalidades para registro e login de usuários, além de integração com um banco de dados SQL Server.

## Índice

- [Tecnologias Utilizadas](#tecnologias-utilizadas)
- [Instalação](#instalação)
- [Configuração do Banco de Dados](#configuração-do-banco-de-dados)
- [Criação de Chaves para JWT](#criação-de-chaves-para-jwt)
- [Endpoints](#endpoints)
- [Como Usar](#como-usar)
- [Contribuição](#contribuição)
- [Licença](#licença)

## Tecnologias Utilizadas

- [.NET 8](https://dotnet.microsoft.com/download/dotnet)
- [ASP.NET Core](https://dotnet.microsoft.com/apps/aspnet)
- [Entity Framework Core](https://docs.microsoft.com/ef/core/)
- [JWT](https://jwt.io/)
- [BCrypt.Net](https://github.com/BcryptNet/bcrypt.net)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

## Instalação

1. Clone o repositório:

   ```bash
   git clone https://github.com/erismaroliveira/DemoJwtRs256.git
   cd DemoJwtRs256
   ```

2. Instale as dependências:

   ```bash
   dotnet restore
   ```

3. Crie o banco de dados (instruções abaixo).

4. Execute a aplicação:

   ```bash
   dotnet run
   ```

## Configuração do Banco de Dados

1. **String de Conexão**: Atualize a string de conexão no arquivo `appsettings.json`:

   ```json
   "ConnectionStrings": {
      "DefaultConnection": "Server=seu_servidor;Database=seu_banco;User Id=seu_usuario;Password=sua_senha;TrustServerCertificate=true;"
    }
   ```

2. **Migrations**: Crie e aplique as migrations:

   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

## Criação de Chaves para JWT

Para usar JWT com RS256, você precisa gerar um par de chaves (uma chave privada e uma chave pública). Você pode fazer isso usando o OpenSSL. Siga os passos abaixo:

1. **Instale o OpenSSL**: Se você não tiver o OpenSSL instalado, você pode baixá-lo em [OpenSSL](https://slproweb.com/products/Win32OpenSSL.html).

2. **Gere a Chave Privada**:

   ```bash
   openssl genpkey -algorithm RSA -out private_key.pem -pkeyopt rsa_keygen_bits:2048
   ```

3. **Gere a Chave Pública**:

   ```bash
   openssl rsa -pubout -in private_key.pem -out public_key.pem
   ```

4. **Adicione as chaves ao seu projeto**: Crie um diretório chamado `Keys` em seu projeto e coloque os arquivos `private_key.pem` e `public_key.pem` dentro dele.

5. **Atualize a configuração do JWT**: No arquivo `appsettings.json`, adicione as seguintes configurações:

   ```json
   "JwtSettings": {
      "Issuer": "seu_issuer",
      "Audience": "seu_audience",
      "PrivateKey": "Chave Privada (base64 encoded)",
      "PublicKey": "Chave Pública (base64 encoded)"
   },
   ```

   - A chave privada deve ser convertida para base64 antes de ser adicionada.

## Endpoints

### Criar Conta

- **Método**: `POST`
- **Endpoint**: `/api/create-account`
- **Body**:
  ```json
  {
    "username": "string",
    "email": "string",
    "password": "string"
  }
  ```

### Login

- **Método**: `POST`
- **Endpoint**: `/api/sessions`
- **Body**:
  ```json
  {
    "username": "string",
    "password": "string"
  }
  ```

## Como Usar

1. **Criar um Usuário**: Envie uma requisição `POST` para `/api/create-account` com os detalhes do usuário no corpo da requisição.

2. **Fazer Login**: Envie uma requisição `POST` para `/api/sessions` com o nome de usuário e senha para obter um token JWT.

3. **Acessar Recursos Protegidos**: Utilize o token JWT obtido no login para acessar recursos protegidos, adicionando-o no cabeçalho `Authorization` da requisição:
   ```makefile
   Authorization: Bearer seu_token_jwt
   ```

## Contribuição

Sinta-se à vontade para abrir uma issue ou enviar um pull request para melhorias e correções de bugs.

## Licença

Este projeto está licenciado sob a [MIT License](https://opensource.org/license/mit).

### Considerações Finais

- **Certifique-se de substituir os placeholders (`seu_servidor`, `seu_banco`, `seu_usuario`, `sua_senha`, `seu_issuer`, e `seu_audience`) pelos valores apropriados em sua configuração.**
