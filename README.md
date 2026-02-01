# Caching-Proxy

This is an implementation of a very simplified proxy server.

The tool accepts the following named args

--port <port>
if unset, assumes 5050

--origin <address> 
Address to where the requests will be redirected
if unset will assume https://dummyjson.com/

--clear-cache
if provided, will erase the db file that contains the "cache"

That is my implementation in C# for the challenge that can be found here
https://roadmap.sh/projects/caching-server
