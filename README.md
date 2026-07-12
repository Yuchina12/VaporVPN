# VaporVPN

Welcome to VaporVPN. This is a new VPN tool but is in beta, so please don't complain!

![VaporVPN Logo](assets/logo.png)

## How to add VaporVPN?
1. Go to the website and download the latest release.
2. Open the VaporVPN v1.3.1 executable.
3. Choose the custom VaporVPN Ethernet adapter and a city, then connect.

## How to add VaporVPN from GitHub?
1. Open the GitHub repository.
2. Open the download folder.
3. Install the vaporvpn_v1.1.exe file.

## Inside App
1. VPN Tool
2. Network adapter choices including VaporVPN Ethernet Adapter #IDNUMBER
3. Location selection for Calgary, Halifax, Montreal, Ottawa, Toronto, Vancouver, and Winnipeg

## Project files
- [index.html](index.html) — landing page
- [download/vaporvpn_v1.3.1.exe](download/vaporvpn_v1.3.1.exe) — downloadable Windows app
- [.github/workflows/pages.yml](.github/workflows/pages.yml) — GitHub Pages deployment
- [VaporVPNApp](VaporVPNApp) — source project for the Windows app

## Publish to GitHub Pages
1. Push this folder to the repository.
2. In GitHub, enable Pages with the GitHub Actions workflow.
3. Open the published URL from the Actions deployment.

## Release workflow
Tag a new release and push the tag to create a GitHub Release automatically via Actions. Example:

```powershell
git tag v1.3.1
git push origin v1.3.1
```

The workflow will build the Windows exe and attach it to the Release.
