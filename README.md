# ModularMonolith
Modular monolith sample. 

Master branch contains version based on Autofac DI container, db transactions, Assembly.Load for MediatR and AutoMapper.
Run CreateDatabase.sql to start application.

DistributedTransactions branch contains version based on MS DI container, TransactionScope and registered in DI containers assembly markers for MediatR and AutoMapper ([difference from master](https://github.com/denis-tsv/ModularMonolith/pull/2/files)).

Compensation branch contains version based on compensation instead of transactions ([difference from DistributedTransactions](https://github.com/denis-tsv/ModularMonolith/pull/3/files)). 

Saga branch contains version based on state machine instead of compensation ([difference from Compensation](https://github.com/denis-tsv/ModularMonolith/pull/4/files)). 
