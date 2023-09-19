import { TphPoly_RootAbstract } from './tph-poly-root-abstract.entity';
import { Entity, PrimaryGeneratedColumn, Column } from 'typeorm';

@Entity('tph_poly_base_class_non_abstract')
export class TphPoly_BaseClassNonAbstract extends TphPoly_RootAbstract {
  @PrimaryGeneratedColumn('uuid')
  id: string;

  @Column()
  baseField: string;

}