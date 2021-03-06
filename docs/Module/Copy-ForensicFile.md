---
external help file: PowerForensics-help.xml
online version: https://github.com/Invoke-IR/PowerForensics/blob/master/Modules/PowerForensics/docs/Copy-ForensicFile.md
schema: 2.0.0
---

# Copy-ForensicFile

## SYNOPSIS
{{Fill in the Synopsis}}

## SYNTAX

### ByPath
```
Copy-ForensicFile [-Path] <String> [-Destination] <String>
```

### ByIndex
```
Copy-ForensicFile [-VolumeName <String>] [-Index] <Int32> [-Destination] <String>
```

## DESCRIPTION
{{Fill in the Description}}

## EXAMPLES

### Example 1
```
PS C:\> {{ Add example code here }}
```

{{ Add example description here }}

## PARAMETERS

### -Destination
{{Fill Destination Description}}

```yaml
Type: String
Parameter Sets: (All)
Aliases: 

Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Index
{{Fill Index Description}}

```yaml
Type: Int32
Parameter Sets: ByIndex
Aliases: 

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Path
{{Fill Path Description}}

```yaml
Type: String
Parameter Sets: ByPath
Aliases: FullName

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -VolumeName
{{Fill VolumeName Description}}

```yaml
Type: String
Parameter Sets: ByIndex
Aliases: 

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## INPUTS

### None


## OUTPUTS

### System.Object

## NOTES

## RELATED LINKS

