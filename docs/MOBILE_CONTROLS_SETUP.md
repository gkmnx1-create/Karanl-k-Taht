# Mobil prototip sahnesi

Unity Editor'de projeyi açtıktan sonra üst menüden **Karanlik Taht → Build Mobile Prototype Scene** seçin.

Oluşan sahne:
- Assets/Scenes/KaranlikTaht_Prototype.unity

Sahnede geçici kapsül karakter, zemin, takip/orbit kamera, sol joystick, sağ kamera sürükleme alanı, basılı tutmalı koşma düğmesi ve EventSystem otomatik oluşturulur.

## Test
1. Sahneyi açın.
2. Play'e basın.
3. Unity Game penceresinde sol joystick'i sürükleyerek hareketi deneyin.
4. Ekranın sağ yarısında sürükleyerek kamerayı çevirin.
5. Sağ alttaki KOŞ düğmesine basılı tutun.

## Notlar
- Karakter şimdilik geçici kapsüldür; nihai erkek/kadın savaşçı ve büyücü modelleri değildir.
- Bu editör aracı sahneyi otomatik üretmek içindir. Burada Unity Editor çalıştırılmadığı için sahne üretimi ve derleme fiilen doğrulanmış değildir.
- Android APK için Unity Android Build Support kurulumu, Android Build Settings ve gerçek cihaz testi gerekir.
