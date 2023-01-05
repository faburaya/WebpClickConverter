#pragma once

using namespace System;
using namespace System::Runtime::InteropServices;

namespace Reusable
{
	namespace MediaCodecs
	{
		namespace Webp
		{
			namespace NativeInterop
			{
				/// <summary>
				/// Verwickelt die Aufrufe zum WebP-API.
				/// </summary>
				public ref class DecoderWrapper
				{
				public:
					/// <summary>
					/// Decodiert den Inhalt einer WEBP-Datei und wandelt ihn in BGRA um.
					/// </summary>
					/// <param name="imageFilePath">Der Pfad der WEBP-Datei.</param>
					/// <param name="widthPixels">Die Breite des Bilds in Pixels.</param>
					/// <param name="heightPixels">Die Höhe des Bilds in Pixels.</param>
					/// <returns>Die decodierten Daten.</returns>
					static array<Byte>^ DecodeWebpToBgra(
						String^ imageFilePath,
						[Out] Int32 %widthPixels,
						[Out] Int32 %heightPixels);
				};
			}
		}
	}
}

