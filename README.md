# RegexFiles

Search for matches in threat Intelligence sources (malicious IoCs, domain and IP blacklists).

Windows desktop executable, developed in C#, that allows to search for matches in plain text files. You can select two directories, the directory where the texts you want to find are located, and the directory where the IoCs are located, and from these two paths, all files will be considered for the search recursively. 

That is, if you have many files with multiple lines of text, such as in CyberSecurity with threat intelligence sources and IoC lists, which can be URLs or IPs, this tool allows you to compare all the text lines of several files of a directory, with all the text lines of several files that contain black lists of IoCs, at one time.

ATTENTION - This tool is a simple C# code that is not mature enough, be careful with the consumption of resources of your windows, it does not have limits on RAM consumption, will use about the files you select. Feel free to continue developing it and improving it.

In this repository you will only find this simple search tool, the files with malicious IoCs you must download them updated from another source (for example https://threatfeeds.io/).
