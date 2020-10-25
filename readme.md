# This is a little playground

I was trying to understand and create a little API from Udemy content. Everything I did learn from that course was applying on this simples backend API.

I have a long run to learn everything on this framework, still I'm liking very much all the new stuff and the way of doing thing on the dotnet.

Hope you like it, any questions send me a DM on twitter.

# Do you want to test it? But first...

What you need:

- Git
- Dotnet Core 3.1
- Dotnet EF
- An IDE (something like VSCode)

### GIT. You know, right?

Just clone this repository.

### Dotnet Core 3.1

Download from the link.

### Dotnet EF

After installing Dotnet Core 3.1, install globally the Entity Framework (EF) on your machine, you can add this with the following command:

```sh
dotnet tool install --global dotnet-ef
```

### An IDE

My recommendations is to use Visual Studio Code, add the following plugins (extensions):

- This
- That

# Then

You need to create the migration and start the local database.

To create the migration use the following command:

```sh
dotnet ef migrations add Initial
```

This should create the right files to make the build the database.

After creating the migration you should create the database with:

```sh
dotnet ef database update
```
