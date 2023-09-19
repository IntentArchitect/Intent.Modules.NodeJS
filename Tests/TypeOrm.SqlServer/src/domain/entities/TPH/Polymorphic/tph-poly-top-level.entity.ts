import { TphPoly_RootAbstract } from './tph-poly-root-abstract.entity';
import { Entity, PrimaryGeneratedColumn, Column, OneToMany } from 'typeorm';

@Entity('tph_poly_top_level')
export class TphPoly_TopLevel {
  @PrimaryGeneratedColumn('uuid')
  id: string;

  @Column()
  topField: string;

  @OneToMany(() => TphPoly_RootAbstract, (poly_RootAbstracts) => poly_RootAbstracts.poly_TopLevel, { cascade: ['insert', 'update'] })
  poly_RootAbstracts: TphPoly_RootAbstract[];

  @Column({ nullable: true })
  createdBy?: string;

  @Column({ nullable: true })
  createdDate?: Date;

  @Column({ nullable: true })
  lastModifiedBy?: string;

  @Column({ nullable: true })
  lastModifiedDate?: Date;
}