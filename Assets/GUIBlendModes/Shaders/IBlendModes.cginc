#ifndef BLEND_MODES
#define BLEND_MODES

float G (float4 c) { return .299 * c.r + .587 * c.g + .114 * c.b; }
 
float4 Darken (float4 a, float4 b) // ~
{ 
	float4 r = float4(.0, .0, .0, b.a);
	r.r = min(a.r, b.r);
	r.g = min(a.g, b.g);
	r.b = min(a.b, b.b);
	return r;
}
float4 Multiply (float4 a, float4 b) // ~
{ 
	float4 r = float4(.0, .0, .0, b.a);
	r.r = a.r * b.r;
	r.g = a.g * b.g;
	r.b = a.b * b.b;
	return r;
}
float4 ColorBurn (float4 a, float4 b) 
{ 
	float4 r = float4(.0, .0, .0, b.a);
	r.r = 1.0 - (1.0 - a.r) / b.r;
	r.g = 1.0 - (1.0 - a.g) / b.g;
	r.b = 1.0 - (1.0 - a.b) / b.b;
	return r;
}
float4 LinearBurn (float4 a, float4 b) // ~
{ 
	float4 r = float4(.0, .0, .0, b.a);
	r.r = a.r + b.r - 1.0;
	r.g = a.g + b.g - 1.0;
	r.b = a.b + b.b - 1.0;
	return r;
}
float4 DarkerColor (float4 a, float4 b) 
{ 
	float4 r = G(a) < G(b) ? a : b;
	r.a = b.a;
	return r; 
}
float4 Lighten (float4 a, float4 b) // ~
{ 
	float4 r = float4(.0, .0, .0, b.a);
	r.r = max(a.r, b.r);
	r.g = max(a.g, b.g);
	r.b = max(a.b, b.b);
	return r;
}
float4 Screen (float4 a, float4 b) 
{ 
	float4 r = float4(.0, .0, .0, b.a);
	r.r = 1.0 - (1.0 - a.r) * (1.0 - b.r);
	r.g = 1.0 - (1.0 - a.g) * (1.0 - b.g);
	r.b = 1.0 - (1.0 - a.b) * (1.0 - b.b);
	return r;
}
float4 ColorDodge (float4 a, float4 b) 
{ 
	float4 r = float4(.0, .0, .0, b.a);
	r.r = a.r / (1.0 - b.r);
	r.g = a.g / (1.0 - b.g);
	r.b = a.b / (1.0 - b.b);
	return r;
}
float4 LinearDodge (float4 a, float4 b) // ~
{ 
	float4 r = float4(.0, .0, .0, b.a);
	r.r = a.r + b.r;
	r.g = a.g + b.g;
	r.b = a.b + b.b;
	return r;
} 
float4 LighterColor (float4 a, float4 b) 
{ 
	float4 r = G(a) > G(b) ? a : b;
	r.a = b.a;
	return r; 
}
float4 Overlay (float4 a, float4 b) 
{
    float4 r = float4(.0, .0, .0, b.a);
    if (a.r > .5) r.r = 1.0 - (1.0 - 2.0 * (a.r - .5)) * (1.0 - b.r);
    else r.r = (2.0 * a.r) * b.r;
    if (a.g > .5) r.g = 1.0 - (1.0 - 2.0 * (a.g - .5)) * (1.0 - b.g);
    else r.g = (2.0 * a.g) * b.g;
    if (a.b > .5) r.b = 1.0 - (1.0 - 2.0 * (a.b - .5)) * (1.0 - b.b);
    else r.b = (2.0 * a.b) * b.b;
    return r;
}
float4 SoftLight (float4 a, float4 b)
{
	float4 r = float4(.0, .0, .0, b.a);
	r.r = (1.0 - a.r) * a.r * b.r + a.r * (1.0 - (1.0 - a.r) * (1.0 - b.r));
	r.g = (1.0 - a.g) * a.g * b.g + a.g * (1.0 - (1.0 - a.g) * (1.0 - b.g));
	r.b = (1.0 - a.b) * a.b * b.b + a.b * (1.0 - (1.0 - a.b) * (1.0 - b.b));
	return r;
}
float4 HardLight (float4 a, float4 b)
{
	float4 r = float4(.0, .0, .0, b.a);
	if (b.r > .5) r.r = 1.0 - (1.0 - a.r) * (1.0 - 2.0 * (b.r - .5));
	else r.r = a.r * (2.0 * b.r);
	if (b.g > .5) r.g = 1.0 - (1.0 - a.g) * (1.0 - 2.0 * (b.g - .5));
	else r.g = a.g * (2.0 * b.g); 
	if (b.b > .5) r.b = 1.0 - (1.0 - a.b) * (1.0 - 2.0 * (b.b - .5));
	else r.b = a.b * (2.0 * b.b);
	return r;
}
float4 VividLight (float4 a, float4 b)
{
	float4 r = float4(.0, .0, .0, b.a);
	if (b.r > .5) r.r = a.r / (1.0 - (b.r - .5) * 2.0);
	else r.r = 1.0 - (1.0 - a.r) / (b.r * 2);
	if (b.g > .5) r.g = a.g / (1.0 - (b.g - .5) * 2.0);
	else r.g = 1.0 - (1.0 - a.g) / (b.g * 2);
	if (b.b > .5) r.b = a.b / (1.0 - (b.b - .5) * 2.0);
	else r.b = 1.0 - (1.0 - a.b) / (b.b * 2);
	return r;
}
float4 LinearLight (float4 a, float4 b)
{
	float4 r = float4(.0, .0, .0, b.a);
	if (b.r > .5) r.r = a.r + 2.0 * (b.r - .5);
	else r.r = a.r + 2.0 * b.r - 1;
	if (b.g > .5) r.g = a.g + 2.0 * (b.g - .5);
	else r.g = a.g + 2.0 * b.g - 1;
	if (b.b > .5) r.b = a.b + 2.0 * (b.b - .5);
	else r.b = a.b + 2.0 * b.b - 1;
	return r;
}
float4 PinLight (float4 a, float4 b)
{
	float4 r = float4(.0, .0, .0, b.a);
	if (b.r > .5) r.r = max(a.r, 2.0 * (b.r - .5));
	else r.r = min(a.r, 2.0 * b.r);
	if (b.g > .5) r.g = max(a.g, 2.0 * (b.g - .5));
	else r.g = min(a.g, 2.0 * b.g);
	if (b.b > .5) r.b = max(a.b, 2.0 * (b.b - .5));
	else r.b = min(a.b, 2.0 * b.b);
	return r;
}
float4 HardMix (float4 a, float4 b)
{
	float4 r = float4(.0, .0, .0, b.a);
	r.r = (b.r > 1.0 - a.r) ? 1.0 : .0;
	r.g = (b.g > 1.0 - a.g) ? 1.0 : .0;
	r.b = (b.b > 1.0 - a.b) ? 1.0 : .0;
	return r;
}
float4 Difference (float4 a, float4 b) 
{ 
	float4 r = float4(.0, .0, .0, b.a);
	r.r = abs(a.r - b.r);
	r.g = abs(a.g - b.g);
	r.b = abs(a.b - b.b);
	return r; 
}
float4 Exclusion (float4 a, float4 b)
{ 
	float4 r = float4(.0, .0, .0, b.a);
	r.r = a.r + b.r - 2.0 * a.r * b.r;
	r.g = a.g + b.g - 2.0 * a.g * b.g;
	r.b = a.b + b.b - 2.0 * a.b * b.b;
	return r; 
}
float4 Subtract (float4 a, float4 b) // ~
{ 
	float4 r = float4(.0, .0, .0, b.a);
	r.r = a.r - b.r;
	r.g = a.g - b.g;
	r.b = a.b - b.b;
	return r; 
}
float4 Divide (float4 a, float4 b)
{ 
	float4 r = float4(.0, .0, .0, b.a);
	r.r = a.r / b.r;
	r.g = a.g / b.g;
	r.b = a.b / b.b;
	return r; 
}
 
#endif
