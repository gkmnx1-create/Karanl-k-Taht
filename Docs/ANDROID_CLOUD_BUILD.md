# Android bulut derlemesi

Unity sürümü: 2022.3 LTS.

## Secrets
GitHub deposunda Settings > Secrets and variables > Actions bölümüne:
- UNITY_LICENSE
- UNITY_EMAIL
- UNITY_PASSWORD

değerleri eklenmelidir. Parola veya lisans metni sohbet içinde paylaşılmamalıdır.

## Çalıştırma
Actions sekmesinden Android APK iş akışını seçip Run workflow başlatın. Başarılı derleme sonrası Actions run içindeki Artifacts bölümünde APK bulunur.

## Not
İlk derleme C# derleme hataları veya eksik sahne/Android ayarları nedeniyle başarısız olabilir. Başarısızsa loglar incelenip düzeltilmelidir.