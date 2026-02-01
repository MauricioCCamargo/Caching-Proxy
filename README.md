# Caching-Proxy

This is an implementation of a CLI tool of a very simplified proxy server in C#.

The tool can be started by running the program passing the args detailed below

## Commands

```bash
--port <port>
```
if unset, assumes 5050

```bash
--origin <address>
``` 
address to where the requests will be redirected
if unset will assume https://dummyjson.com/

```bash
--clear-cache
```
if provided, will erase the db file that contains the "cache"

## Goal
That is my implementation in C# for the challenge that can be found here
https://roadmap.sh/projects/caching-server
