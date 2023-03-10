#include "DecoderWrapper.h"

#include <fstream>
#include <decode.h>

using namespace System;
using namespace System::IO;

static String^ GetErrorDetails(VP8StatusCode status)
{
	switch (status)
	{
	case VP8_STATUS_OK:
		return "VP8_STATUS_OK";
	case VP8_STATUS_OUT_OF_MEMORY:
		return "VP8_STATUS_OUT_OF_MEMORY";
	case VP8_STATUS_INVALID_PARAM:
		return "VP8_STATUS_INVALID_PARAM";
	case VP8_STATUS_BITSTREAM_ERROR:
		return "VP8_STATUS_BITSTREAM_ERROR";
	case VP8_STATUS_UNSUPPORTED_FEATURE:
		return "VP8_STATUS_UNSUPPORTED_FEATURE";
	case VP8_STATUS_SUSPENDED:
		return "VP8_STATUS_SUSPENDED";
	case VP8_STATUS_USER_ABORT:
		return "VP8_STATUS_USER_ABORT";
	case VP8_STATUS_NOT_ENOUGH_DATA:
		return "VP8_STATUS_NOT_ENOUGH_DATA";
	default:
		return "Unbekannter Fehler";
	}
}

array<Byte>^ Decoder::Webp::NativeInterop::DecoderWrapper::DecodeWebpToBgra(
	String^ imageFilePath,
	[Out] Int32% widthPixels,
	[Out] Int32% heightPixels)
{
	array<Byte>^ encodedData = File::ReadAllBytes(imageFilePath);
	const pin_ptr<Byte> pinnedEncodedData = &encodedData[0];
	const Byte *rawEncodedDataPtr = pinnedEncodedData;

	WebPBitstreamFeatures features{};
	const VP8StatusCode rc = WebPGetFeatures(rawEncodedDataPtr, encodedData->Length, &features);
	if (rc != VP8_STATUS_OK)
	{
		throw gcnew Exception(
			String::Format("WebPGetFeatures ist gescheitert: {0}", GetErrorDetails(rc)));
	}

	heightPixels = static_cast<uint32_t>(features.height);
	widthPixels = static_cast<uint32_t>(features.width);

	const uint32_t stride = widthPixels * 4/*bytes per ARGB-Pixel*/;
	const uint32_t byteCount = heightPixels * stride;

	array<Byte>^ decodedData = gcnew array<Byte>(byteCount);
	const pin_ptr<Byte> pinnedDecodedData = &decodedData[0];
	Byte* rawDecodedDataPtr = pinnedDecodedData;

	if (nullptr == WebPDecodeBGRAInto(
		rawEncodedDataPtr, encodedData->Length, rawDecodedDataPtr, decodedData->Length, stride))
	{
		throw gcnew Exception("WebPDecodeBGRAInto ist gescheitert!");
	}
	return decodedData;
}
