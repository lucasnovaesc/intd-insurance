```markdown
# INTD – Plataforma de Seguros (Propostas & Contratações)


## Requisitos
- .NET 8 SDK
- Docker + Docker Compose


## Como subir em Docker
```bash
docker compose up --build
```
- PropostaService: http://localhost:8080/swagger
- ContratacaoService: http://localhost:8081/swagger


> As migrações são aplicadas automaticamente no startup (`Database.Migrate()`).


## Rotas
### PropostaService
- `POST /propostas` cria proposta `{ clienteNome, produtoCodigo, premio }`
- `GET /propostas` lista
- `GET /propostas/{id}` busca
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
- **DB único + schemas**: `proposta` e `contratacao` isolam tabelas por serviço.
- **Comunicação**: REST simples (Contratacao chama Proposta para checar status).
- **Auto-migrate**: simplifica o boot local.


## Próximos passos (bônus)
- Mensageria (RabbitMQ) emitindo evento `PropostaAprovada` → `ContratacaoService` consome e pré-cria rascunhos.
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
PostgreSQL (db=intd, schemas: proposta, contratacao)
```


---


## Observações finais
- Para gerar **migrations**: `dotnet ef migrations add Initial --project src/PropostaService/Infrastructure --startup-project src/PropostaService/Api` (análogo para ContratacaoService).
- Em produção, separar DBs fisicamente é recomendável; aqui mantemos 1 instância para simplificar.
- Trocar HTTP por **filas** é simples: crie um `IPropostaStatusChecker` adapter via mensageria.
