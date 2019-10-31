del ".\SRB_config\*.dll"
del ".\SRB_config\SRB_config.exe"
copy ".\app.publish\SRB_CTR.exe" ".\SRB_config\SRB_config.exe"
copy ".\*.dll" ".\SRB_config\"

del SRB_config.zip 
7z a -tzip SRB_config.zip ".\SRB_config\"

del "D:\lee_doc\lee-doc\Arduino\libraries\SRB-master\SRB_config.zip"
copy SRB_config.zip "D:\lee_doc\lee-doc\Arduino\libraries\SRB-master\"


del "D:\cloud-bank360\V\SRB-Internal-Files\SRB_config.zip"
copy SRB_config.zip "D:\cloud-bank360\V\SRB-Internal-Files\"


pause