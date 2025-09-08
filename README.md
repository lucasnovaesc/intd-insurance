```markdown
# INTD – Plataforma de Seguros (Propostas & Contratações)


## Requisitos
- .NET 8 SDK
- Docker + Docker Compose


## Como subir em Docker
```bash
docker compose up --d
```
- PropostaService: http://localhost:8080/swagger
- ContratacaoService: http://localhost:8081/swagger


> As migrações são aplicadas automaticamente no startup (`Database.Migrate()`).


## Rotas
### PropostaService
- `Delete /productTypes/{id}` delete 
- `POST /productTypes` create product Type `{ name, description }`
- `GET /productTypes` list
- `GET /productTypes/{id}` search
- `PATCH /productTypes/{id}/` 
### PropostaService
- `Delete /products/{id}` delete 
- `POST /products` create product `{ name, description, productPrice, productTypeId }`
- `GET /products` list
- `GET /products/{id}` search
- `PATCH /products/{id}/` 
### PropostaService
- `Delete /propostas/{id}` delete 
- `POST /propostas` create proposta `{ customerId, propoposalNumber, prooductId }`
- `GET /propostas` list
- `GET /propostas/{id}` search
- `PATCH /propostas/{id}/status` body: `0|1|2` (EmAnalise|Aprovada|Rejeitada)


### ContratacaoService
- `POST /contratacoes` body: `{ propostaId }` (só contrata se status = Aprovada)
- `GET /contratacoes` lista
- `GET /contratacoes/{id}` busca


## Testes
```bash
# na raiz com .sln gerado
dotnet test
```


## Arquitetura
- **Hexagonal**: camadas `Domain (Entidades, Ports)` ↔ `Application (UseCases)` ↔ `Adapters/Infra (EF, HTTP)`; API só orquestra UseCases.
- **DDD Tático**: entidades ricas (`Proposta`, `Contratacao`) com invariantes.
- **Clean Code/SOLID**: `Ports` definem contratos; `Adapters` implementam; injeção de dependência.


## Decisões
- **Comunicação**: REST simples (Contratacao chama Proposta para checar status).
- **Auto-migrate**: simplifica o boot local.


## Próximos passos (bônus)
- Autenticação (JWT) e observabilidade (Serilog + OpenTelemetry).
```


---


## Diagrama simples (ASCII)


```text
+-------------------+ REST +------------------------+
| PropostaService | <-----------------------> | ContratacaoService |
| - API | | - API |
| - UseCases | | - UseCases |
| - Ports/Adapters | | - Ports/Adapters |
| - EF (schema p) | | - EF (schema c) |
+---------+---------+ +-----------+------------+
| |
v v
PostgreSQL (db=serviceproposal, schemas: proposta, contratacao)
