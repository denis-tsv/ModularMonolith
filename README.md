# ModularMonolith
Modular monolith sample. 
Run CreateDatabase.sql to start application.

Master branch contains version based db transactions, Assembly.Load for MediatR and AutoMapper.
Run CreateDatabase.sql to start application.

DistributedTransactions branch contains version based TransactionScope and assembly markers registered in DI container for MediatR and AutoMapper ([difference from master](https://github.com/denis-tsv/ModularMonolith/pull/5/files)).

Compensation branch contains version based on compensation instead of transactions ([difference from DistributedTransactions](https://github.com/denis-tsv/ModularMonolith/pull/3/files)). 

Saga branch contains version based on state machine instead of compensation ([difference from Compensation](https://github.com/denis-tsv/ModularMonolith/pull/4/files)). 
