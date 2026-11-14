# Mobil Kontrol Kurulumu

Bu dosyalar kontrol bileşenleridir; sahne ve Inspector bağlantıları Unity Editor'de yapılmalıdır.

## 1. Karakter
1. Karakter kök nesnesine CharacterController ve ThirdPersonMotor ekleyin.
2. UI üzerinde bir joystick arka planı (Image) ve onun altında/üstünde bir tutamak (Image) oluşturun.
3. Joystick nesnesine MobileJoystick ekleyin; Background ve Handle alanlarını Inspector'dan bağlayın.
4. ThirdPersonMotor içindeki Joystick alanına bu bileşeni sürükleyin.
5. Kamera Transform alanına ana kamerayı bağlayın.

## 2. Kamera
1. Ana kameraya ThirdPersonCameraFollow ekleyin.
2. Target alanına karakter kök Transform'unu bağlayın.
3. Canvas üzerinde ekranın sağ tarafını kaplayan şeffaf bir Image oluşturun.
4. Image üzerinde Raycast Target açık olsun; CameraTouchArea bileşenini ekleyin.
5. CameraTouchArea içindeki Camera Follow alanına ana kameradaki ThirdPersonCameraFollow bileşenini bağlayın.
6. Joystick alanının sağ kamera alanının üstüne gelmemesine dikkat edin. Joystick'in Image nesneleri de Raycast Target kullanmalıdır.

## 3. Koşma düğmesi
Bir UI Button oluşturun. OnClick yerine basılı tutma davranışı için EventTrigger veya ayrı bir pointer-down/up bileşeni kullanarak ThirdPersonMotor.SetRun(true/false) çağırın. Şimdilik koşma klavyede Left Shift ile de denenebilir.

## 4. Sahne gereksinimleri
- Canvas: Screen Space - Overlay
- EventSystem: sahnede bulunmalı
- Canvas Scaler: Scale With Screen Size
- Karakterde CharacterController olmalı
- Kamera tag'i MainCamera olmalı

Not: Kodlar GitHub deposuna eklenmiştir ancak Unity Editor'de sahne kurulumu, Inspector referansları, Android cihaz testi ve APK derlemesi henüz yapılmamıştır.
