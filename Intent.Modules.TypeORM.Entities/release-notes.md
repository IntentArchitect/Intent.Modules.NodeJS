### Version 3.3.7

- Better support for migrations, see the `Using Migrations` section below for more information.

### Using Migrations

TypeORM provides migration infrastructure (see https://typeorm.io/migrations). This module adds scripts to `package.json` to make it easy to run various migration commands.

#### Enabling automatic running of migrations

To enable automatic running of migrations on application startup, ensure that the following environment variables are set, either as per your OS or alternatively in the `.env` file:

- `DB_MIGRATIONS_RUN` to `"true"`. This instructs TypeORM to run migrations on initialization.
- `DB_SYNCHRONIZE` to `"false"`. When this is true, then TypeORM will also try synchronize the database in addition to running migrations which causes conflicts in updates to the schema.

#### Migration commands

**Run migrations**

Manually run any outstanding migrations.

```
npm run typeorm:run-migrations
```

**Generate a migration**

Compares the current database schema to the entity configuration and generates a migration script and saves it as the provided name.

```
npm run typeorm:generate-migration --name=migrationName
```

**Create an empty migration**

Creates an empty migration without generating any query for it and saves it as the provided name.

```
npm run typeorm:create-migration --name=migrationName
```

**Revert migrations**

This command will execute `down` in the latest executed migration. If you need to revert multiple migrations you must call this command multiple times.

```
npm run typeorm:revert-migration
```