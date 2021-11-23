# OktaTools

Steps needed after cloning for Build
1. Restore Nuget Packages - right click solution --> click Restore Nuget Packages
2. try to build, it should fail with 1 error - Could not find file 'Secrets.json'
3. If you look at solution explorer, there should be a file named Secrets.json but it missing.
4. Ask for a copy from someone who currently has 1 in their local (Perry Rosales)
5. Open explorer in project's root folder then place a copy of the Secrets.json file.
6. Rebuild should succeed now.
