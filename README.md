# ModularMonolith
Modular monolith sample. 

There are many ways to implement modular monolith, they differs by:
- data consistency (DbTransactions, CommitableTransaction, TransactionScope, Compensation, Saga)
- modules communication (contract, notifications)
- assemblies for MediatR and AutoMapper (search, register in DI container)

Run CreateDatabase.sql to create database structure before start application.

Master branch used DbTransaction for data consistency, search for files by pattern and loading assemblies for MediatR and AutoMapper.

CommitableTransaction-Direct branch based on master and used CommitableTransaction instead of DbTransaction ([diff](https://github.com/denis-tsv/ModularMonolith/pull/6/files)).

CommitableTransaction-Notification branch based on CommitableTransaction-Direct and used MediatR notifications to communicate between modules instead of module contract call ([diff](https://github.com/denis-tsv/ModularMonolith/pull/7/files))

DistributedTransactions branch based on master and used TransactionScope instead of DbTransaction for data consistency and assembly markers registered in DI container for MediatR and AutoMapper ([diff](https://github.com/denis-tsv/ModularMonolith/pull/8/files)).

TransactionScope-ConnectionString-Direct branch based on DistributedTransactions and used connection string instead of connection for DbContect creation ([diff](https://github.com/denis-tsv/ModularMonolith/pull/9/files)).

Compensation branch based on DistributedTransactions and used compensation instead of transactions ([diff](https://github.com/denis-tsv/ModularMonolith/pull/3/files)). 

Saga branch based on Compensation and used state machine instead of compensation ([diff](https://github.com/denis-tsv/ModularMonolith/pull/4/files)). 
