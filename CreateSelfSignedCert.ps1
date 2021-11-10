# Create certificate - must be created in users store ('My'). With Basic Constraints 2.5.29.19 == Other certs can't be derived from this
$cert = New-SelfSignedCertificate -CertStoreLocation "cert:\LocalMachine\My" -TextExtension @("2.5.29.19={text}false") -Type CodeSigningCert  -Subject "CN=MSIX Fast Start"
# Move Cert from Personal Store to Root Store - Cert is now trusted
Move-Item -Path $cert.PSPath -Destination "Cert:\LocalMachine\Root"

# Output cert details
Get-ChildItem -Path "Cert:\LocalMachine\Root" | Where-Object Thumbprint -eq $cert.Thumbprint | Select-Object *

# Create Secure String (Encrypted in memory) password 
$mypwd = ConvertTo-SecureString -String "1234" -Force -AsPlainText
# Export certificate to .pfx file 
Get-ChildItem -Path ("cert:\localMachine\Root\" + $cert.Thumbprint) | Export-PfxCertificate -FilePath .\MyMSIXFastStartCert.pfx -Password $mypwd
