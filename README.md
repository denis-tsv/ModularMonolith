# ModularMonolith
Run CreateDatabase.sql to create database structure before start application.

The most important thing for Modular Monolith is data consistency for workflows which implemented using several modules. There are many ways to achieve data consistency:
- Master branch used DbTransaction
- CommitableTransaction branch based on master and used CommitableTransaction instead of DbTransaction ([diff](https://github.com/denis-tsv/ModularMonolith/pull/10/files))
- TransactionScope-Connection branch based on master and used TransactionScope instead of DbTransaction for data consistency ([diff](https://github.com/denis-tsv/ModularMonolith/pull/11/files))
- TransactionScope-ConnectionString branch based on TransactionScope-Connection and used connection string instead of connection for DbContect creation ([diff](https://github.com/denis-tsv/ModularMonolith/pull/12/files))
- Compensation branch based on TransactionScope-ConnectionString and used compensation instead of transactions ([diff](https://github.com/denis-tsv/ModularMonolith/pull/13/files))
- Saga branch based on Compensation and used state machine instead of compensation ([diff](https://github.com/denis-tsv/ModularMonolith/pull/14/files))
