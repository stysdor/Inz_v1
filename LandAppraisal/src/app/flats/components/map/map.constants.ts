export const center = { lat: 50.033611, lng: 22.004722 };

export const defaultBounds: google.maps.LatLngBoundsLiteral = {
  north: center.lat + 2,
  south: center.lat - 2,
  east: center.lng + 2,
  west: center.lng - 2,
};
export const options: google.maps.places.AutocompleteOptions = {
  bounds: defaultBounds,
  componentRestrictions: { country: "pl" },
  fields: ["address_components", "geometry", "icon", "name"],
  strictBounds: false,
  types: ["establishment"]
};
