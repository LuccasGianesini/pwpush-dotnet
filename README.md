# PW PUSH DOTNET

This cli uses [pwpush](https://pwpush.com/) project to securely share secrets with other people.

## Pw Push Api

* [Api Docs](https://pwpush.com/api)

## How to use
Download the binary for your operating system in the [releases](https://github.com/LuccasGianesini/pwpush-dotnet/releases/) page of the repository.

Binaries are available for `windows`, `linux` and `osx`.

> Quick tip: Rename the binaries to something simple like `pwpush` and `pwget` and put them in a folder added to you PATH environment variable! 

To use the pwpush cli and push a secret, you just need to invoke the binary and provide the secret to be protected:

`pwpush.exe "super-secret-value"`

And to get a secret you just need to provide the url token generated when you pushed a secret:

`pwget.exe super-secret-url-token`

### CLI Flags

#### PwPush

The pwpush binary pushes secrets to the secure enclave

* `-d`: Number of days that the value will be available to be queried in a range from `1` to `90` (defaults to `1`);
* `-v`: Number of views that the secret can have (defaults to `2`);
* `-p`: Secret password required to view the secret (no default);
* `-u`: Url of the pwpush enclave (defaults to https://pwpush.com/);
* `-r`: Indicates if the secret can be removed from the enclave by the user (defaults to true);

#### PwGet

The pwget binary gets secrets from the secure enclave

* `-p`: Secret password required to view the secret (no default);
* `-u`: Url of the pwpush enclave (defaults to https://pwpush.com/);
