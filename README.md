# Dakota Hub
FrontEnd + BackEnd + Automações

# Deploy da Aplicação

## Ferramentas Usadas

- **Vercel**:
  - **Deploy do Frontend**: Implementação do projeto frontend desenvolvido com Vite e React, utilizando o ambiente de hospedagem da Vercel, que é otimizado para projetos JavaScript modernos. Com suporte integrado para CI/CD, a Vercel automatiza o processo de deploy, facilitando a entrega contínua e atualizações rápidas da aplicação.

<!-- ## Links

- **Frontend:**
  - [https://nao_existe.app/](https://teste-nuti.vercel.app/)

- **Backend:**
  - [https://nao_existe.onrender.com/](https://backend-nuti.onrender.com/) -->

# Regras de Negócio

- <strong> Pesquisa de Ferramentas: </strong>

- <strong> Histórico de Pesquisas: </strong> 

# Requisitos Funcionais

  ### Home Page

  - <strong>RF01:</strong> Possuir navbar dinâmico e interativo com redirecionamento de página e scroll automático;
    
  - <strong>RF02:</strong> Possuir "Hero Section
 para apresentar os objetivos da plataforma de pesquisa e avaliação de custo-benefício;

  - <strong>RF03:</strong> Possuir uma "Seção About" informando sobre como funciona a plataforma;

  ### Login Page

  - <strong>RF04:</strong> Deve possuir um forms de login para a inserção dos dados, para a validação de acesso + opção de recuperar conta e cadastrar-se;

  - <strong>RF05:</strong> Deve ter links para redirecionamento como: "Return", "Forgot your password", "Register here"


# Requisitos Não Funcionais

- <strong>RNF01:</strong>
  - Uso da linguagem de programação C# + Framework ASP .NET CORE para a elaboração da API

- <strong>RNF02:</strong>
  - Uso do padrão RestFull para o desenvolvimento da API

- <strong>RNF03:</strong>
  - Uso da linguagem de programação Javascript junto com as linguagens de marcação como HTML5 e CSS3 para o desenvolvimento do Front-End juntamente com a biblioteca ReactJS


# Visual do Website



## Visual Desktop

Segue abaixo está alguns screenshots relativo à interface do website produzido da "Dakota Hub":

### Home Page:

![Home Page](./assets/HomePage.png)

### Login Page:

![Login Page](./assets/LoginPage.png)

## Visual Mobile

Segue abaixo está alguns screenshots relativo à interface do website produzido da "Dakota Hub":

### Home Page:



### Login Page:


# Sobre Banco de Dados

## Modelagem Conceitual

### DER (Diagrama Entidade-Relacionamento)
![Diagrama de Entidade-Relacionamento](./info_db/der/der.jpg)

#### Dicionário de Entidades e Relacionamentos

Abaixo estão detalhadas as informações sobre as entidades do sistema e seus relacionamentos, facilitando o entendimento da modelagem de dados.

## Dicionário de Entidades e Relacionamentos

| **Entidade**     | **Relacionamento C.**       | **Nome do Relacionamento** | **Descrição**                                                   |
|------------------|-----------------------------|----------------------------|-----------------------------------------------------------------|
| Usuário          | Checklist                    | Cria                       | Usuário pode criar uma ou mais checklists.                      |
| Cliente          | Checklist                    | Responde                   | Várias checklists são respondidas por vários clientes.          |
| Checklist        | Questão                      | Tem                        | Uma checklist tem uma ou mais questões.                         |
| Questão          | Resposta_alternativa         | Tem                        | Uma questão pode receber uma ou mais respostas.                 |
| Interação        | Resposta_alternativa         | Tem                        | Várias respostas pertencem a uma determinada interação.         |
| Usuário          | Cliente                      | Possui                     | Um usuário possui um ou mais clientes.                          |


#### Dicionário de Tabelas e Atributos

A seguir, são descritas as tabelas do banco de dados e seus respectivos atributos. Esta seção visa auxiliar no entendimento da modelagem e no cálculo do espaço de armazenamento necessário.

## Dicionário de Dados

**users**

- `id` (INT, PRIMARY KEY, IDENTITY(1,1)): Identificador único do usuário.
- `username` (VARCHAR(50), NOT NULL): Nome de usuário, com até 50 caracteres.
- `password` (VARCHAR(60), NOT NULL): Senha do usuário, com até 60 caracteres.
- `email` (VARCHAR(120), UNIQUE, NOT NULL): E-mail do usuário, com até 120 caracteres.

**roles**

- `id` (INT, PRIMARY KEY, IDENTITY(1,1)): Identificador único da função (role).
- `role_name` (VARCHAR(50), NOT NULL): Nome da função, com até 50 caracteres.

**users_have_roles**

- `id_users` (INT, FOREIGN KEY): Referência ao identificador do usuário na tabela `users`.
- `id_roles` (INT, FOREIGN KEY): Referência ao identificador da função na tabela `roles`.

**organization**

- `id` (INT, PRIMARY KEY, IDENTITY(1,1)): Identificador único da organização.
- `name` (VARCHAR(50), NOT NULL): Nome da organização, com até 50 caracteres.
- `website` (VARCHAR(255), NOT NULL): URL do website da organização, com até 255 caracteres.
- `logo` (VARBINARY(MAX), NOT NULL): Logotipo da organização, armazenado em formato binário.

**ai**

- `id` (INT, PRIMARY KEY, IDENTITY(1,1)): Identificador único da inteligência artificial (AI).
- `id_organization` (INT, FOREIGN KEY): Referência ao identificador da organização na tabela `organization`.
- `name` (VARCHAR(20), NOT NULL): Nome da inteligência artificial, com até 20 caracteres.
- `logo` (VARBINARY(MAX), NOT NULL): Logotipo da inteligência artificial, armazenado em formato binário.

**category**

- `id` (INT, PRIMARY KEY, IDENTITY(1,1)): Identificador único da categoria.
- `name` (VARCHAR(50), NOT NULL): Nome da categoria, com até 50 caracteres.
- `description` (VARCHAR(80), NOT NULL): Descrição da categoria, com até 80 caracteres.

**category_have_ai**

- `id_category` (INT, FOREIGN KEY): Referência ao identificador da categoria na tabela `category`.
- `id_ai` (INT, FOREIGN KEY): Referência ao identificador da AI na tabela `ai`.

**plan**

- `id` (INT, PRIMARY KEY, IDENTITY(1,1)): Identificador único do plano.
- `id_ai` (INT, FOREIGN KEY): Referência ao identificador da AI na tabela `ai`.
- `price` (MONEY): Preço do plano.
- `time` (INT, NOT NULL): Tempo de validade do plano, em dias.


## Modelagem Lógica

![Modelo Lógico](./info_db/logic/model_logic.png)

## Modelagem Física

    ```sql

    CREATE TABLE users (
        id INT PRIMARY KEY IDENTITY(1,1),
        username VARCHAR(50) NOT NULL,
        password VARCHAR(60) NOT NULL,
        email VARCHAR(120) NOT NULL UNIQUE
    );

    CREATE TABLE roles (
        id INT PRIMARY KEY IDENTITY(1,1),
        role_name VARCHAR(50) NOT NULL
    );

    CREATE TABLE users_have_roles (
        id_users INT NOT NULL, 
        id_roles INT NOT NULL, 
        CONSTRAINT fk_id_users_in_users_have_roles FOREIGN KEY (id_users) REFERENCES users (id), 
        CONSTRAINT fk_id_roles_in_users_have_roles FOREIGN KEY (id_roles) REFERENCES roles (id)
    );

    CREATE TABLE organization (
        id INT PRIMARY KEY IDENTITY (1,1), 
        name VARCHAR(50) NOT NULL, 
        website VARCHAR(255) NOT NULL, 
        logo VARBINARY(MAX) NOT NULL
    );

    CREATE TABLE ai (
        id INT PRIMARY KEY IDENTITY(1,1), 
        id_organization INT NOT NULL, 
        name VARCHAR(20) NOT NULL, 
        logo VARBINARY(MAX) NOT NULL, 
        CONSTRAINT fk_id_organization_in_ai FOREIGN KEY (id_organization) REFERENCES organization (id)
    );

    CREATE TABLE category (
        id INT PRIMARY KEY IDENTITY(1, 1),
        name VARCHAR(50) NOT NULL, 
        description VARCHAR(80) NOT NULL
    );

    CREATE TABLE category_have_ai (
        id_category INT NOT NULL, 
        id_ai INT NOT NULL, 
        CONSTRAINT fk_id_category_in_category_have_ai FOREIGN KEY (id_category) REFERENCES category (id), 
        CONSTRAINT fk_id_ai_in_category_have_ai FOREIGN KEY (id_ai) REFERENCES ai (id)
    );

    CREATE TABLE plans (
        id INT PRIMARY KEY IDENTITY(1, 1),
        id_ai INT NOT NULL, 
        price MONEY, 
        time INT NOT NULL,
        CONSTRAINT fk_id_ai_in_plan FOREIGN KEY (id_ai) REFERENCES ai (id)
    );


    ```
